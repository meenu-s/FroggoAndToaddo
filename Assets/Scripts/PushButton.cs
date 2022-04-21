using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPressed;
    public float goalPosition = -1.5f;

    void Start()
    {
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "froggo" || collision.gameObject.tag == "toaddo")
        {
            isPressed = true;
        }
    }   

    void OnCollisionExit2D(Collision2D collision)
    {
        isPressed = false;
    }
}

