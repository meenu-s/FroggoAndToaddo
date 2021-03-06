using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadMovement : MonoBehaviour
{
    Animator m_Animator;
    public CharacterController2D t_controller;
    public float t_runSpeed = 1f;

    float t_horizontalMove = 0f;
    bool t_jump = false;
    public bool t_moving = false;
    float movementError = 0.0001f;

    void Start()
    {
        //This gets the Animator, which should be attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        if (CheckpointManager.Instance.t_RespawnPoint != null ) 
        {
            transform.position = CheckpointManager.Instance.t_RespawnPoint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // HorizontalToad and JumpToad keys are configured in UnityEditor > Edit > Project Settings > InputManager
        // get horizontal speed and whether currently jumping or not
        t_horizontalMove = t_runSpeed * Input.GetAxisRaw("HorizontalToad");

        // set t_moving to true or false depending on if the toad is currently moving or not
        if (Mathf.Abs(t_horizontalMove)>movementError)
        {
            t_moving = true;
        } else 
        {
            t_moving = false;
        }

        //float moveInput = Input.GetAxisRaw("Horizontal");
        // if (Input.GetKeyDown(KeyCode.A)){
        //     Debug.Log("toad is moving");
        //     m_Animator.SetBool("moving", false);
        // } else {
        //     Debug.Log("toad is not moving");
        //     m_Animator.SetBool("moving", true);
        // }

        if (Input.GetButtonDown("JumpToad"))
        {
            t_jump = true;
        }
        
        // Change size of the toad TODO: put this in another file
        if (Input.GetButtonDown("ToadSize"))
        {
            // Toggle through 3 toad sizes
            Vector3 currentSize = transform.localScale;
            Vector3 newSize;
            int signOfX = currentSize.x < 0 ? -1 : 1;

            // Set sizes here: 0.08, 0.15, 0.25
            float smallScale = 0.08f;
            float mediumScale = 0.15f;
            float largeScale = 0.25f;
            float error = 0.01f;

            // What size is toad currently in 
            if (Mathf.Abs(currentSize.x)-smallScale < error) {
                // currently in small size, resize to medium size
                newSize = new Vector3( signOfX * mediumScale, mediumScale, 1f );
            } else if (Mathf.Abs(currentSize.x)-mediumScale < error) {
                // currently in medium size, resize to large size
                newSize = new Vector3( signOfX * largeScale, largeScale, 1f );
            } else if (Mathf.Abs(currentSize.x)-largeScale < error) {
                // currently in large size, resize to small size
                newSize = new Vector3( signOfX * smallScale, smallScale, 1f );
            } else {
                // this size is not valid, resize to medium size
                Debug.Log("help: the toad should not be this size of "+ currentSize.x);
                newSize = new Vector3( signOfX * mediumScale, mediumScale, 1f );
                Debug.Log("defaulting toad to medium size");
            }
            // set new size
            transform.localScale = newSize;
        }
        
        // Check if level restarted
        if (Input.GetButtonDown("Restart"))
        {
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "FirstLevel"));
        }
    }

    void FixedUpdate() 
    {
        // if (!t_controller.gameOver()) 
        // {
            if (t_controller.isInWater() || t_controller.justDied()) {
                Debug.Log("TOAD DIED");
                
                // when Toad Falls in water and dies, restart
                StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "DeathScene"));
                // GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().StopMusic();
                GameObject.Find("Audio Source").GetComponent<MusicManager>().StopMusic();
            }
            // Move our character
            t_controller.Move(t_horizontalMove, false, t_jump);
            t_jump = false;

        // }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CheckpointManager.Instance.EnterTrigger("Toad", other, transform.position);
    }
}
