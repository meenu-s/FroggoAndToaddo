using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoButtonActivatedPlatform : MonoBehaviour
{
    public PushButton button1;
    public PushButton button2;


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
        if (button1.isPressed && button2.isPressed)
        {
            if (Mathf.Abs(button1.goalPosition - currentXPos) > error)
            {
                if (button1.goalPosition - currentXPos > 0)
                {
                    currentXPos += platformSpeed;

                }
                else
                {
                    currentXPos += -platformSpeed;
                }
            }
        }
        else{
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

