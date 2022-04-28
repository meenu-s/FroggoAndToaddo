using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLever : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isToggled;
    public bool withinBounds;
    public float goalPosition = -1.5f;

    void Start()
    {
        isToggled = false;
        withinBounds = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && withinBounds)
        {
            print("mouse");
            isToggled = !isToggled;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // print($"collide {collision.gameObject.tag}");
        withinBounds = true;
    }

        void OnTriggerExit2D(Collider2D collision)
    {
        // print($"collide {collision.gameObject.tag}");
        withinBounds = false;
    }
}

