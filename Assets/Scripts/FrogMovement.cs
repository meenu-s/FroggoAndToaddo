using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    public CharacterController2D f_controller;
    public float f_runSpeed = 1f;
    float f_horizontalMove = 0f;
    bool f_jump = false;
    bool f_swimDown = false;
    // bool f_climb = false;
    public bool f_moving = false;
    float movementError = 0.0001f;

    // Update is called once per frame
    void Update()
    {
        // HorizontalToad and JumpToad keys are configured in UnityEditor > Edit > Project Settings > InputManager
        // get horizontal speed and whether currently jumping or not
        f_horizontalMove = f_runSpeed * Input.GetAxisRaw("HorizontalFrog");

        // set t_moving to true or false depending on if the toad is currently moving or not
        if (Mathf.Abs(f_horizontalMove)>movementError)
        {
            f_moving = true;
        } else 
        {
            f_moving = false;
        }

        if (Input.GetButtonDown("JumpFrog"))
        {
            f_jump = true;
        } if (Input.GetButtonDown("SwimDownFrog")) {
            f_swimDown = true;
            Debug.Log("down button pushes");
        } 
        // if (Input.GetButtonDown("ClimbFrog")) {
        //     f_climb = true;
        //     Debug.Log("space button pushed");
        // }
    }

    void FixedUpdate() 
    {
        // Move our character
        // f_controller.Move(f_horizontalMove, false, f_jump, f_swimDown, f_climb);
        f_controller.Move(f_horizontalMove, false, f_jump, f_swimDown);
        f_jump = false;
        f_swimDown = false;
        // f_climb = false;
    }
}
