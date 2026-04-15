using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public CarController player;
    public void OnMenuButton()
    {
        SceneLoader.Instance.LoadScene("Menu");
        player.carControls.Disable();
    }
}
