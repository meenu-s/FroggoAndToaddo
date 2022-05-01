using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public static CheckpointManager Instance;
    public Vector3 t_RespawnPoint = new Vector3(-8.5f, -2.5f, 0f);                  // respawn point for toad
    public Vector3 f_RespawnPoint = new Vector3(-7.5f, -2.5f, 0f);                  // respawn point for frog
    private Collider2D t_LastCheckpoint;            // last checkpoint hit for toad
    private Collider2D f_LastCheckpoint;            // last checkpoint hit for frog
    private Vector3 RespawnOffset = new Vector3(.5f, 0f, 0f);                  // magnitude of respawn point offset

    private void Awake() {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this; 
        DontDestroyOnLoad(gameObject); 
        t_RespawnPoint = new Vector3(-8.5f, -2.5f, 0f);
        f_RespawnPoint = new Vector3(-7.5f, -2.5f, 0f);
    }

    /**
        Updates respawn point when trigger collision is detected
        Inputs: (string) character - "Toad" or "Frog" depending on which character is colliding
                (Collider2D) collision - the trigger collision detected by toad or frog
                (Vector3) position - the position of the character
    **/
    public void EnterTrigger(string character, Collider2D collision, Vector3 position)
    {
        // check if the collision is with a checkpoint
        if(collision.tag == "checkpoint")
        {
            Debug.Log("Checkpoint triggered by " + character);
            // if yes, update the last checkpoint depending on if the character is toad or frog 
            if (character=="Toad")
                t_LastCheckpoint = collision;
            else if (character=="Frog") 
                f_LastCheckpoint = collision;
            
            // Check if the last checkpoints hit by toad and frog were the smae
            if (t_LastCheckpoint.name==f_LastCheckpoint.name) {
                // if yes, update the respawn points
                t_RespawnPoint = position - RespawnOffset;
                f_RespawnPoint = position + RespawnOffset;
            }
        }
        
    }
}
