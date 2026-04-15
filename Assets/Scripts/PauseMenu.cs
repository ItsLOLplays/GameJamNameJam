using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    private CarControls controls;
    public GameObject pauseMenuUI;
    public Camera gameCamera;
    public AudioMixer audioMixer;
    public GameObject gameplayUI;

    private static float volume;

    private void Start()
    {
        controls = new CarControls();
        controls.Enable();
        audioMixer.GetFloat("Volume", out volume);

        if (volume == 0)
        {
            audioMixer.SetFloat("Volume", -80f);
        }
        else
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(volume / 100f) * 20);
        }
        Resume();
    }

    void Update()
    {
        if (controls.Car.Pause.triggered)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void OnResumeButton()
    {
        Resume();
    }

    public void OnSettingsButton()
    {
        pauseMenuUI.SetActive(false);
        gameCamera.enabled = false;
        SceneManager.sceneLoaded += OnSettingsScreenLoaded;
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    private void OnSettingsScreenLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Settings") return;
        SceneManager.sceneLoaded -= OnSettingsScreenLoaded;

        FindObjectsByType<SettingsBackButton>().First().onBack = OnSettingsClosed;
    }

    private void OnSettingsClosed()
    {
        SceneManager.UnloadSceneAsync("Settings")!.completed += _ =>
        {
            gameCamera.enabled = true;
            pauseMenuUI.SetActive(true);
        };
    }

    public void OnQuitButton()
    {
        SceneLoader.Instance.LoadScene("Menu");
        controls.Disable();
    }

    public void Resume()
    {
        gameplayUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        
        volume = PlayerPrefs.GetFloat("Volume", volume);

        if (volume == 0)
        {
            audioMixer.SetFloat("Volume", -80f);
        }
        else
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(volume / 100f) * 20f);
        }
    }

    void Pause()
    {
        gameplayUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        audioMixer.SetFloat("Volume", -80f);
    }
}
