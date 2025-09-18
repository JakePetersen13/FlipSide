using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Win"))
        {
            StartCoroutine(WaitAndLoad(5f, "Home"));
        }
    }

    private IEnumerator WaitAndLoad(float time, string sceneName)
    {
        yield return new WaitForSeconds(time);
        FindObjectOfType<LevelTransition>().LoadNextLevel(sceneName);
    }


    public void StartGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect);
        FindObjectOfType<LevelTransition>().LoadNextLevel("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
