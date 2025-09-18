using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelTransition : MonoBehaviour
{
    public Image transitionOverlay;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadNextLevel(string nextSceneName)
    {
        StartCoroutine(FadeOutAndLoad(nextSceneName));
    }

    IEnumerator FadeIn()
    {
        float time = fadeDuration;
        Color color = transitionOverlay.color;

        while (time > 0) 
        {
            time -= Time.deltaTime;
            color.a = Mathf.Clamp01(time / fadeDuration);
            transitionOverlay.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOutAndLoad(string nextSceneName)
    {
        try
        {
            FindObjectOfType<PlayerController>().enabled = false;
        }
        catch { }

        float time = 0f;
        Color color = transitionOverlay.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Clamp01(time / fadeDuration);
            transitionOverlay.color = color;
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }



}
