using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject currentMenu;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

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
        if (currentMenu!=null) {
            currentMenu.SetActive(false);
        }
        pauseMenuUI.SetActive(false);
        SetTime(1f);
        gameIsPaused = false;
    }

    public void Retry()
    {
        Resume();
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

    public void LevelSelector() {
        SceneManager.LoadScene("LevelSelector");
    }

    private void SetTime(float time) {
        Time.timeScale = time;
    }
}
