using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPlatformMovement : MonoBehaviour
{
    public PushButton[] pushButtons;

    // Start is called before the first frame update
    public float defaultYPos = -1.5f;
    public float currentYPos = -1.5f; //initial platform height
    public float platformSpeed = 0.1f;

    public float error = 0.01f;


    void Start()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, defaultYPos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        bool noButtonsPressed = true;
        for (int i = 0; i < pushButtons.Length; i++)
        {
            if (pushButtons[i].isPressed)
            {
                if (Mathf.Abs(pushButtons[i].goalPosition - currentYPos) > error)
                {
                    if (pushButtons[i].goalPosition - currentYPos > 0)
                    {
                        currentYPos += platformSpeed;

                    }
                    else 
                    {
                        currentYPos += -platformSpeed;
                    }
                }
                noButtonsPressed = false;
                break;
            }
        }
        if (noButtonsPressed)
        {
            if (Mathf.Abs(defaultYPos - currentYPos) > error)
            {

                if (defaultYPos - currentYPos > 0)
                {
                    currentYPos += platformSpeed;

                }
                else if (defaultYPos - currentYPos < 0)
                {
                    currentYPos += -platformSpeed;
                }
            }
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, currentYPos, 0);


    }


}

