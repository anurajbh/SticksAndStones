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
    public Animator animator;
	public static bool teleporting = false;
	public static PlayerMovement Instance;

    public Rigidbody2D rb2D;

    private float tileConstant = 32f/100f*6.5f;
    public bool CheckFreeze()
    {
        bool b;

        if (DialogueManager.triggered)
        {
            b = true;
        }
        else
        {
            b = false;
        }
        return b;
    }

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        movePoint.parent = null;
        if (Instance == null)
        {
           Instance = this;
           DontDestroyOnLoad(gameObject);
           DontDestroyOnLoad(movePoint);
        }
        else
        {
          Destroy(gameObject);
        }
    }
    
    private void FixedUpdate()
    {
        if (CheckFreeze()) { return; }
        if (!teleporting) {
            var position = Vector3.MoveTowards(transform.position, movePoint.position, walkSpeed * Time.deltaTime);
            rb2D.MovePosition((Vector2)position);
            CheckForMovementInput();
            CheckForPlayerDirection();
        } else {
            rb2D.position = movePoint.position;
        }
    }

    private void CheckForPlayerDirection()
    {
        if(Math.Abs(CrossPlatformInputManager.GetAxisRaw("Vertical")) == 0f)
        {
            animator.SetBool("North", false);
            animator.SetBool("South", false);
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0f)
            {
                animator.SetBool("East", true);
                animator.SetBool("West", false);
            }
            else if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0f)
            {
                animator.SetBool("West", true);
                animator.SetBool("East", false);
            }
            else
            {
                animator.SetBool("West", false);
                animator.SetBool("East", false);
            }
        }
        else if (Math.Abs(CrossPlatformInputManager.GetAxisRaw("Horizontal")) == 0f)
        {
            animator.SetBool("West", false);
            animator.SetBool("East", false);
            if (CrossPlatformInputManager.GetAxisRaw("Vertical") > 0f)
            {
                animator.SetBool("North", true);
                animator.SetBool("South", false);
            }
            else if (CrossPlatformInputManager.GetAxisRaw("Vertical") < 0f)
            {
                animator.SetBool("South", true);
                animator.SetBool("North", false);
            }
            else
            {
                animator.SetBool("South", false);
                animator.SetBool("North", false);
            }
        }

    }

    private void CheckForMovementInput()
    {
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.5f && !inMenu)
        {
            if (Mathf.Abs(CrossPlatformInputManager.GetAxisRaw("Horizontal")) == 1f)
            {
				if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0f, 0f), 0f, whatStopsYou))
                {
                    movePoint.position += new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0f, 0f);
				}
            }
            else if (Mathf.Abs(CrossPlatformInputManager.GetAxisRaw("Vertical")) == 1f)
            {
				if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, CrossPlatformInputManager.GetAxisRaw("Vertical"), 0f), 0f, whatStopsYou))
                {
                    movePoint.position += new Vector3(0f, CrossPlatformInputManager.GetAxisRaw("Vertical"), 0f);
				} 
            }
        }
        

    }
}
   