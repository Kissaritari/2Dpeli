using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public AudioSource btnSound;
    public void StartGame()
    {
        btnSound.Play();
        SceneManager.LoadScene(2);

    }
    public void QuitGame()
    {
        btnSound.Play();
        Application.Quit();
    }
}
