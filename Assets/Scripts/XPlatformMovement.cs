using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPlatformMovement : MonoBehaviour
{
    public PushButton[] pushButtons;

    // Start is called before the first frame update
    public float defaultXPos = -1.5f;
    public float currentXPos = -1.5f; //initial platform height
    public float platformSpeed = 0.1f;

    public float error = 0.01f;


    void Start()
    {
        gameObject.transform.position = new Vector3(defaultXPos, gameObject.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        bool noButtonsPressed = true;
        for (int i = 0; i < pushButtons.Length; i++)
        {
            if (pushButtons[i].isPressed)
            {
                if (Mathf.Abs(pushButtons[i].goalPosition - currentXPos) > error)
                {
                    if (pushButtons[i].goalPosition - currentXPos > 0)
                    {
                        currentXPos += platformSpeed;

                    }
                    else
                    {
                        currentXPos += -platformSpeed;
                    }
                }
                noButtonsPressed = false;
                break;
            }
        }
        if (noButtonsPressed)
        {
            if (Mathf.Abs(defaultXPos - currentXPos) > error)
            {

                if (defaultXPos - currentXPos > 0)
                {
                    currentXPos += platformSpeed;

                }
                else if (defaultXPos - currentXPos < 0)
                {
                    currentXPos += -platformSpeed;
                }
            }
        }

        gameObject.transform.position = new Vector3(currentXPos, gameObject.transform.position.y, 0);


    }


}

