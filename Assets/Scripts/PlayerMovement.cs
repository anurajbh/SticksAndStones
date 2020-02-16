using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public GameObject PlayerBody;
    public float walkSpeed =5f;
    public float sprintSpeed = 5f;// for future sprint implementation
    private void Awake()
    {
        
    }
    private void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()//TO-DO- Sprint Implementation
    {
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");
        float xMovement = xPos * walkSpeed * Time.deltaTime;
        float yMovement = yPos * walkSpeed * Time.deltaTime;

        float xMove = transform.localPosition.x + xMovement;
        float yMove = transform.localPosition.y + yMovement;
        transform.localPosition = new Vector2(xMove, yMove);
    }

}
