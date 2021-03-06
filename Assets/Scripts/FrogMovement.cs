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

    void Start()
    {
        //This gets the Animator, which should be attached to the GameObject you are intending to animate.
        if (CheckpointManager.Instance.f_RespawnPoint != null ) 
        {
            transform.position = CheckpointManager.Instance.f_RespawnPoint;
        }
    }

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
        } 
        // if (Input.GetButtonDown("ClimbFrog")) {
        //     f_climb = true;
        //     Debug.Log("space button pushed");
        // }
    }

    void FixedUpdate() 
    {
        if (f_controller.justDied()) {
            Debug.Log("FROG DIED");
            
            // when Toad Falls in water and dies, restart
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "DeathScene"));
            // GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().StopMusic();
            GameObject.Find("Audio Source").GetComponent<MusicManager>().StopMusic();
        }
        // Move our character
        // f_controller.Move(f_horizontalMove, false, f_jump, f_swimDown, f_climb);
        f_controller.Move(f_horizontalMove, false, f_jump, f_swimDown);
        f_jump = false;
        f_swimDown = false;
        // f_climb = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CheckpointManager.Instance.EnterTrigger("Frog", other, transform.position);
    }
}
