using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    Resolution[] resolutions;
    
    private void Start()
    { 
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentIndex = 0;
        var options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();
        
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 50f);
    }

    public void OnBackButton()
    {
        if (SceneManager.GetSceneByName("Settings").isLoaded && SceneManager.sceneCount > 1)
        {
            SceneLoader.Instance.UnloadAndGoBack();
        }
        else
        {
            SceneLoader.Instance.GoBack();
        }
    }

    public void SetVolume(float percent)
    {
        PlayerPrefs.SetFloat("Volume", percent);
    }

    public void ToggleFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetScreenResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
