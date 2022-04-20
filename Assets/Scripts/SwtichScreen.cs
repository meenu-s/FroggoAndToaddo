using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwtichScreen : MonoBehaviour
{
    public void HomeScene() {
        SceneManager.LoadScene("HomeScene");
    }

    public void HowToScene() {
        SceneManager.LoadScene("HowToScene");
    }

    public void LevelScene() {
        SceneManager.LoadScene("FirstLevel");
    }

    public void testScene() {
        SceneManager.LoadScene("MovementTesting");
    }
}