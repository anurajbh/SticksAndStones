using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public bool inMenu = false;
    //public bool hasMoved = false;
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
        if(Vector3.Distance(transform.position, movePoint.position)<=0.5f && !inMenu)
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
   