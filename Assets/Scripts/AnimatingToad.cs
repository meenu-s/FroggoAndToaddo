using System.Collections;
using System.Collections.Generic;
//Set up a new Boolean parameter in the Unity Animator and name it, in this case “Jump”.
//Set up transitions between each state that the animation could follow. For example, the player could be running or idle before they jump, so both would need transitions into the animation.
//If the “Jump” boolean is set to true at any point, the m_Animator plays the animation. However, if it is ever set to false, the animation would return to the appropriate state (“Idle”).
//This script enables and disables this boolean in this case by listening for the mouse click or a tap of the screen.

using UnityEngine;

public class AnimatingToad : MonoBehaviour
{
    //Fetch the Animator
    Animator m_Animator;

    void Start()
    {
        //This gets the Animator, which should be attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        
        //If the GameObject is not jumping, send that the Boolean “Jump” is false to the Animator. The jump animation does not play.
        if (GameObject.Find("Toaddo").GetComponent<ToadMovement>().t_moving){
            m_Animator.SetBool("moving", true);
        } else {
            m_Animator.SetBool("moving", false);
        }
        
    }
}
