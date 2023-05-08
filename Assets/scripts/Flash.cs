using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    private Image _image = null;
    private Coroutine _currentFlashCoroutine;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    public void StartFlash(float flashDuration, float maxAlpha, Color newColor)
    {
        _image.color = newColor;
        maxAlpha = Mathf.Clamp(maxAlpha, 0f, 1f);

        if (_currentFlashCoroutine != null)
            StopCoroutine(_currentFlashCoroutine);
        _currentFlashCoroutine = StartCoroutine(SingleFlash(flashDuration, maxAlpha));
    }
    public void DeathColor(float flashDuration, float maxAlpha, Color newColor)
    {
        _image.color = newColor;
        maxAlpha = Mathf.Clamp(maxAlpha, 0f, 1f);

        //if (_currentFlashCoroutine != null)
        // StopCoroutine(_currentFlashCoroutine);
        _currentFlashCoroutine = StartCoroutine(stay(flashDuration, maxAlpha));
    }

    IEnumerator SingleFlash(float oneFlashDuration, float maxAlpha)
    {
        float flashDuration = oneFlashDuration / 2;
        for (float t = 0; t <= flashDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t / flashDuration);
            _image.color = colorThisFrame;
            yield return null;
        }

        float flashOutDuration = oneFlashDuration / 2;
        for (float t = 0; t <= flashDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, t / flashOutDuration);
            _image.color = colorThisFrame;
            yield return null;
        }

        _image.color = new Color32(0, 0, 0, 0);
    }
    IEnumerator stay(float oneFlashDuration, float maxAlpha)
    {
        float flashDuration = oneFlashDuration / 2;
        for (float t = 0; t <= flashDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t / flashDuration);
            _image.color = colorThisFrame;
            yield return null;
        }
    }
}
