using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private bool coroutineFinished;

    public async void LoadMainMenu() {
        await LoadLevelWithName("MainMenu");
    }

    public async void LoadPreviousLevel() {
        await LoadLevel(SceneManager.GetActiveScene().buildIndex-1);
    }

    public async void LoadNextLevelAsync() {
        if(SceneManager.sceneCountInBuildSettings-1 != SceneManager.GetActiveScene().buildIndex) {
            await LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
        }
        else {
            LoadMainMenu();
        }
    }

    public async Task LoadLevel(int levelIndex) {
        //load level with transition
        transition.SetTrigger("Start");
        await Task.Delay(TimeSpan.FromSeconds(transitionTime));
        SceneManager.LoadScene(levelIndex);
        await Task.Run(() => new WaitUntil(() => coroutineFinished));
    }

    public async Task LoadLevelWithName(string levelName) {
        //load level with transition
        transition.SetTrigger("Start");
        await Task.Delay(TimeSpan.FromSeconds(transitionTime));
        SceneManager.LoadScene(levelName);
    }
}
