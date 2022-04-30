using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwtichScreen : MonoBehaviour
{
    public void HomeScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "HomeScene"));
        GameObject.Find("Audio Source").GetComponent<MusicManager>().PlayMusic();
        // GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
        // SceneManager.LoadScene("HomeScene");
    }

    public void HowToScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "HowToScene"));
        GameObject.Find("Audio Source").GetComponent<MusicManager>().PlayMusic();
        // GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
        // SceneManager.LoadScene("HowToScene");
    }

    public void CreditsScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Credits"));
        GameObject.Find("Audio Source").GetComponent<MusicManager>().PlayMusic();
        // GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
        // SceneManager.LoadScene("Credits");
    }

    public void LevelScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "FirstLevel"));
        GameObject.Find("Audio Source").GetComponent<MusicManager>().PlayMusic();
        // GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
        // SceneManager.LoadScene("FirstLevel");
    }

    public void testScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "MovementTesting"));
        GameObject.Find("Audio Source").GetComponent<MusicManager>().PlayMusic();
        // GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
        // SceneManager.LoadScene("MovementTesting");
    }

    public void deathScene() {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "DeathScene"));
        GameObject.Find("Audio Source").GetComponent<MusicManager>().PlayMusic();
        // GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().StopMusic();
        // SceneManager.LoadScene("MovementTesting");
    }
}