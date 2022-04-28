using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwtichScreen : MonoBehaviour
{
    public void HomeScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "HomeScene"));
        // SceneManager.LoadScene("HomeScene");
    }

    public void HowToScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "HowToScene"));
        // SceneManager.LoadScene("HowToScene");
    }

    public void CreditsScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Credits"));
        // SceneManager.LoadScene("Credits");
    }

    public void LevelScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "FirstLevel"));
        // SceneManager.LoadScene("FirstLevel");
    }

    public void testScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "MovementTesting"));
        // SceneManager.LoadScene("MovementTesting");
    }
}