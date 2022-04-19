using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoButtonPush : MonoBehaviour
{
    public PushButton[] pushButtons;

    // Start is called before the first frame update
    public float defaultHeight = -1.5f;
    public float currentHeight = -1.5f; //initial platform height
    public float platformSpeed = 0.1f;


    void Start()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, defaultHeight, 0);
        /*GameObject[] buttonObjs  = GameObject.FindGameObjectsWithTag("button");
        pushButtons = new PushButton[buttonObjs.Length];
        for (int i = 0; i < buttonObjs.Length; i++)
        {
            pushButtons[i] = buttonObjs[i].GetComponent<PushButton>();
        }
        print(pushButtons.Length);*/


    }

    // Update is called once per frame
    void Update()
    {
        bool noButtonsPressed = true;
        for (int i = 0; i < pushButtons.Length; i++)
        {
            if (pushButtons[i].isPressed)
            {
                if (currentHeight != pushButtons[i].goalHeight)
                {
                    if (pushButtons[i].goalHeight - currentHeight > 0)
                    {
                        currentHeight += platformSpeed;

                    }
                    else
                    {
                        currentHeight += -platformSpeed;
                    }
                }
                noButtonsPressed = false;
                break;
            }
        }
        if (noButtonsPressed)
        {
            if (defaultHeight - currentHeight > 0)
            {
                currentHeight += platformSpeed;

            }
            else if(defaultHeight - currentHeight <0)
            {
                currentHeight += -platformSpeed;
            }
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, currentHeight, 0);


    }


}

