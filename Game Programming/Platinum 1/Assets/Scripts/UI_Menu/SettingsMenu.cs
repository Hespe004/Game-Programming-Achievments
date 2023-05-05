using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject audioMenuUI;
    [SerializeField] private GameObject keybindMenuUI;
    [SerializeField] private AudioMixer audioMixer;
    
    //Resolution variables
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    int currentResolutionIndex = 0;

    void Start() {
        //get resolutions
        resolutions = Screen.resolutions;

        //clear placeholder items in dropdown
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for(int i = 0; i < resolutions.Length; i++) {
            Debug.Log("Resolution: " + resolutions[i]);
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution(int resolutionIndex) {
        //resoltuionIndex gives a number of which item of the choices it is
        // 1080 = 0, 3440 = 1, etc
        Debug.Log(resolutionIndex);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void SetFullscreen(bool isFullscreen) {
        Debug.Log(isFullscreen);
        Screen.fullScreen = isFullscreen;
    }

    public void BackToPause()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void BackToSettings()
    {
        keybindMenuUI.SetActive(false);
        audioMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
}
