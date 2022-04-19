using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public GameObject platform;
    // Start is called before the first frame update
    public int toggledHeight = 0.5;
    public int initHeight = 0;
    void Start()
    {
        platform = GameObject.FindWithTag("platform");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            platform.transform.position = new Vector3(0, toggledHeight, 0);
        }
        else
        {
            platform.transform.position = new Vector3(0, initHeight, 0);
        }
    }
}
