using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    
    private string previousScene;
    private string currentScene;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        currentScene = SceneManager.GetActiveScene().name;
    }

    public void LoadScene(string sceneName)
    {
        previousScene = currentScene;
        currentScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAdditive(string sceneName)
    {
        previousScene = currentScene;
        currentScene = sceneName;
        Time.timeScale = 0f;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadAndGoBack()
    {
        string sceneToUnload = currentScene;
        currentScene = previousScene;
        previousScene = null;
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }

    public void GoBack()
    {
        if (!string.IsNullOrEmpty(previousScene)) LoadScene(previousScene);
    }
}
