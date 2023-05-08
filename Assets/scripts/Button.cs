using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    bool painettu = false;
    public AudioSource btnSound;
    public GameObject poistoikkuna;
    private void Start()
    {
        painettu = false;
    }
    public void StartGame()
    {
        _ = StartCoroutine(nameof(sceneAani),2);

    }
    public void MainMenu()
    {
        _ = StartCoroutine(nameof(sceneAani), 0);

    }
    public void ClearStats()
    {
        btnSound.Play();
        PlayerPrefs.DeleteAll();
        _= StartCoroutine(nameof(poistoFlash));
    }
    public void QuitGame()
    {
        btnSound.Play();
        Application.Quit();
    }

    private IEnumerator sceneAani(int scenenro)
    {
        if (!btnSound.isPlaying && painettu == false)
        {
            btnSound.Play();
            painettu = true;
        }
        yield return new WaitUntil(()=>btnSound.isPlaying == false);
        SceneManager.LoadScene(scenenro);
    }
    private IEnumerator poistoFlash()
    {
        poistoikkuna.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        poistoikkuna.SetActive(false);
    }
}
