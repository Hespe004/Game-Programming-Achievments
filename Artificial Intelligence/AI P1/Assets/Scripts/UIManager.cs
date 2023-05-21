using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void PlayLevel(float levelNumber) {
        SceneManager.LoadScene("Level-" + levelNumber.ToString());
    }

    public void PlayNextLevel() {
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        else {
            MainMenu();
        }
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
