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
        // HorizontalToad and JumpToad keys are configured in UnityEditor > Edit > Project Settings > InputManager
        // get horizontal speed and whether currently jumping or not
        horizontalMove = runSpeed * Input.GetAxisRaw("HorizontalToad");
        if (Input.GetButtonDown("JumpToad"))
        {
            jump = true;
        }
        
        // Change size of the toad TODO: put this in another file
        if (Input.GetButtonDown("ToadSize"))
        {
            // Toggle through 3 toad sizes
            Vector3 currentSize = transform.localScale;
            Vector3 newSize;

            // Set sizes here: 0.08, 0.15, 0.25
            float smallScale = 0.08f;
            float mediumScale = 0.15f;
            float largeScale = 0.25f;
            float error = 0.01f;

            // What size is toad currently in 
            if (Mathf.Abs(currentSize.x-smallScale) < error) {
                // currently in small size, resize to medium size
                newSize = new Vector3( mediumScale, mediumScale, 1f );
                Debug.Log("Toad is now medium");
            } else if (Mathf.Abs(currentSize.x-mediumScale) < error) {
                // currently in medium size, resize to large size
                newSize = new Vector3( largeScale, largeScale, 1f );
                Debug.Log("Toad is now large");
            } else if (Mathf.Abs(currentSize.x-largeScale) < error) {
                // currently in large size, resize to small size
                newSize = new Vector3( smallScale, smallScale, 1f );
                Debug.Log("Toad is now small");
            } else {
                // this size is not valid, resize to medium size
                Debug.Log("help: the toad should not be this size");
                newSize = new Vector3( mediumScale, mediumScale, 1f );
                Debug.Log("defaulting toad to medium size");
            }
            // set new size
            transform.localScale = newSize;
        }
    }

    void FixedUpdate() 
    {
        // Move our character
        controller.Move(horizontalMove, false, jump);
        jump = false;
    }
}
