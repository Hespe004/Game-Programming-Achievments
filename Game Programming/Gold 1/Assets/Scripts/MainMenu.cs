using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame() {
        SceneManager.LoadScene("Level-1-Scene");
    }

    public void HowToPlay() {
        SceneManager.LoadScene("How to play");
    }

    public void QuitGame() {
        Debug.Log("Quiting the game!");
        Application.Quit();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }
}
