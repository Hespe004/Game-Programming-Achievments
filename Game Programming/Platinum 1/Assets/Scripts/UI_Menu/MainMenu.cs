using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;
    public void OnPlay() => levelLoader.LoadNextLevelAsync();
    public async void OnPlaySkipTutorial() => await levelLoader.LoadLevelWithName("Level-0");

    public void OnQuit() {
        // UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Quiting the game");
        Application.Quit();
    }
}
