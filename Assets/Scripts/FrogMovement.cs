using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 1f;
    float horizontalMove = 0f;
    bool jump = false;
    bool swimDown = false;

    // Update is called once per frame
    void Update()
    {
        // HorizontalToad and JumpToad keys are configured in UnityEditor > Edit > Project Settings > InputManager
        // get horizontal speed and whether currently jumping or not
        horizontalMove = runSpeed * Input.GetAxisRaw("HorizontalFrog");
        if (Input.GetButtonDown("JumpFrog"))
        {
            jump = true;
        } if (Input.GetButtonDown("SwimDownFrog")) {
            swimDown = true;
            Debug.Log("down button pushes");
        }
    }

    void FixedUpdate() 
    {
        // Move our character
        controller.Move(horizontalMove, false, jump, swimDown);
        jump = false;
        swimDown = false;
    }
}
