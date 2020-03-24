using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
   
    public float walkSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsYou;
    private void Awake()
    {
        movePoint.parent = null;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position,  walkSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, movePoint.position)<=0.5f)
        {
            if (Math.Abs(CrossPlatformInputManager.GetAxisRaw("Horizontal")) == 1f && Math.Abs(CrossPlatformInputManager.GetAxisRaw("Vertical")) == 0f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0f, 0f), 0.5f, whatStopsYou))
                {
                    movePoint.position += new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
            else if (Math.Abs(CrossPlatformInputManager.GetAxisRaw("Vertical")) == 1f && Math.Abs(CrossPlatformInputManager.GetAxisRaw("Horizontal")) == 0f)
            {
               if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, CrossPlatformInputManager.GetAxisRaw("Vertical"), 0f), 0.5f, whatStopsYou))
               {
                    movePoint.position += new Vector3(0f, CrossPlatformInputManager.GetAxisRaw("Vertical"), 0f);
               }
            }
        }
    }
       
}
    /*public bool hasMoved = false;
    private Vector2 movementInput;

    private Vector3 movementdirection;
    private void Awake()
    {
        
    }
    private void Update()
    {
        GetPlayerInput();
        if(movementInput.x == 0 && movementInput.y == 0)
        {
            hasMoved = false;
        }
        else if ((movementInput.x !=0 && !hasMoved) || (movementInput.y != 0 && !hasMoved) )
        {
            hasMoved = true;
            GetPlayerDirection();
        }
        
    }

    private void GetPlayerInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    private void GetPlayerDirection()
    {
        if (movementInput.x < 0)
        {
            if (movementInput.y < 0)
            {
                movementdirection = new Vector3(-walkSpeed / 2f, -walkSpeed / 2f);
            }
            else if (movementInput.y > 0)
            {
                movementdirection = new Vector3(-walkSpeed / 2f, walkSpeed / 2f);
            }
            else
            {
                movementdirection = new Vector3(-walkSpeed, 0f);
            }
            transform.position += movementdirection;
        }
        if (movementInput.x > 0)
        {
            if (movementInput.y < 0)
            {
                movementdirection = new Vector3(walkSpeed / 2f, -walkSpeed / 2f);
            }
            else if (movementInput.y > 0)
            {
                movementdirection = new Vector3(walkSpeed / 2f, walkSpeed / 2f);
            }
            else
            {
                movementdirection = new Vector3(walkSpeed, 0f);
            }
            transform.position += movementdirection;
        }
        if (movementInput.y > 0)
        {
            if (movementInput.x < 0)
            {
                movementdirection = new Vector3(-walkSpeed / 2f, walkSpeed / 2f);
            }
            else if (movementInput.x > 0)
            {
                movementdirection = new Vector3(walkSpeed / 2f, walkSpeed / 2f);
            }
            else
            {
                movementdirection = new Vector3(0f, walkSpeed);
            }
            transform.position += movementdirection;
        }
        if (movementInput.y < 0)
        {
            if (movementInput.x < 0)
            {
                movementdirection = new Vector3(-walkSpeed / 2f, -walkSpeed / 2f);
            }
            else if (movementInput.x > 0)
            {
                movementdirection = new Vector3(walkSpeed / 2f, -walkSpeed / 2f);
            }
            else
            {
                movementdirection = new Vector3(0f, -walkSpeed);
            }
            transform.position += movementdirection;
        }
        /*float xPos = Input.GetAxisRaw("Horizontal");//to ensure that movement input is discrete
        float yPos = Input.GetAxisRaw("Vertical");//to ensure that movement input is discrete
        float xMovement = xPos * walkSpeed * Time.deltaTime;
        float yMovement = yPos * walkSpeed * Time.deltaTime;

        float xMove = transform.localPosition.x + xMovement;
        float yMove = transform.localPosition.y + yMovement;
        transform.localPosition = new Vector3(xMove, yMove, -0.1f);*/


