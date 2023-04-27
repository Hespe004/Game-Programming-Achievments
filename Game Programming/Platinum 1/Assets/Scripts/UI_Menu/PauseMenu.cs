using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject settingsMenuUI;
    public GameObject pauseMenuUI;
    public LevelLoader levelLoader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        SetTime(0f);
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        SetTime(1f);
        gameIsPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("quitting the game");
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Restart()
    {
        SetTime(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        Resume();
        //await levelLoader.LoadLevelWithName("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenSecretLevel() {
        GameManager.instance.SetTimeScale(1f);
        //await levelLoader.LoadLevelWithName("Level-Secret");
        SceneManager.LoadScene("Level-Secret");
    }

    public void Settings() {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    // Method for universally setting the time
    private void SetTime(float time) {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.SetTimeScale(time);
        }
    }
}
