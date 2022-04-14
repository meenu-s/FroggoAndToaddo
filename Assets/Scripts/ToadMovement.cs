using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public float runSpeed = 1f;

    float horizontalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        // code with coded key button presses
        // int movementDirection = 0;
        // if (Input.GetKey("left"))
        // {
        //     movementDirection = -1;
        // } else if (Input.GetKey("right"))
        // {
        //     movementDirection = 1;
        // }
        // horizontalMove = runSpeed * movementDirection;
        // if (Input.GetKeyDown("up"))
        // {
        //     jump = true;
        // }
        
        // original code from tutorial
        // horizontalMove = runSpeed * Input.GetAxisRaw("Horizontal");
        // if (Input.GetButtonDown("Jump"))
        // {
        //     jump = true;
        // }

        // HorizontalToad and JumpToad keys are configured in UnityEditor > Edit > Project Settings > InputManager
        // get horizontal speed and whether currently jumping or not
        horizontalMove = runSpeed * Input.GetAxisRaw("HorizontalToad");
        if (Input.GetButtonDown("JumpToad"))
        {
            jump = true;
        }
    }

    void FixedUpdate() 
    {
        // Move our character
        controller.Move(horizontalMove, false, jump);
        jump = false;
    }
}
