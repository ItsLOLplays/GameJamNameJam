using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private SceneLoader sceneLoader;
    public GameObject wrapper;

    public void OnPlayButton()
    {
        SceneLoader.Instance.LoadScene("Game");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
    public void OnSettingsButton()
    {
        SceneManager.sceneLoaded += OnSettingsScreenLoaded;
        SceneLoader.Instance.LoadSceneAdditive("Settings");
        wrapper.SetActive(false);
    }

    private void OnSettingsScreenLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Settings") return;
        SceneManager.sceneLoaded -= OnSettingsScreenLoaded;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
        
        var backButton = FindObjectsByType<SettingsBackButton>().FirstOrDefault();

        backButton!.onBack = OnSettingsClosed;
    }

    private void OnSettingsClosed()
    {
        wrapper.SetActive(true);
        SceneManager.UnloadSceneAsync("Settings");
    }
}
