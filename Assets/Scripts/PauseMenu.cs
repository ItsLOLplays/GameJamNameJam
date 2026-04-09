using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    private CarControls controls;
    public GameObject pauseMenuUI;
    public Camera gameCamera;
    public AudioMixer audioMixer;

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
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        audioMixer.SetFloat("Volume", -80f);
    }
}
