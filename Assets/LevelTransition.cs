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
        float t = fadeDuration;
        Color c = transitionOverlay.color;

        while (t > 0)
        {
            t -= Time.deltaTime;
            c.a = Mathf.Clamp01(t / fadeDuration);
            transitionOverlay.color = c;
            yield return null;
        }
    }

    IEnumerator FadeOutAndLoad(string nextSceneName)
    {
        FindObjectOfType<PlayerController>().enabled = false;
        float t = 0f;
        Color c = transitionOverlay.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Clamp01(t / fadeDuration);
            transitionOverlay.color = c;
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
