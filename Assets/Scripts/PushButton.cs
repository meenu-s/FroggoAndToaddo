using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public GameObject platform;
    // Start is called before the first frame update
    public float currentHeight = -1.5f; //initial platform height
    public float lowPlatformHeight = -1.5f;
    public float highPlatformHeight = 0f;
    private float currentPlatformSpeed = 0f;
    public float platformSpeed = 0.1f;


    void Start()
    {
        platform = GameObject.FindWithTag("platform");
        platform.transform.position = new Vector3(platform.transform.position.x, currentHeight, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHeight < highPlatformHeight && currentHeight > lowPlatformHeight)
        {
            currentHeight += currentPlatformSpeed;
        }
        else if (currentHeight < lowPlatformHeight)
        {
            currentHeight = lowPlatformHeight;
        }
        else if (currentHeight > highPlatformHeight)
        {
            currentHeight = highPlatformHeight;
        }
        else
        {
            currentPlatformSpeed = 0f;
        }
        platform.transform.position = new Vector3(platform.transform.position.x, currentHeight, 0);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "froggo" || collision.gameObject.tag == "toaddo")
        {
            currentPlatformSpeed = platformSpeed;
            currentHeight += currentPlatformSpeed;
        }
    }   

    void OnCollisionExit2D(Collision2D collision)
    {
        currentPlatformSpeed = -platformSpeed;
        currentHeight += currentPlatformSpeed;
    }
}

