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
        // Fade in from black at level start
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
        FindObjectOfType<PlayerController>().enabled = false;
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
