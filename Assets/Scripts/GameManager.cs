using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int targetScore = 3;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
            scoreText.text = "collected: " + score + "/" + targetScore;

        if (score == targetScore)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level1":
                    FindObjectOfType<LevelTransition>().LoadNextLevel("Level2");
                    break;
                case "Level2":
                    FindObjectOfType<LevelTransition>().LoadNextLevel("Level3");
                    break;
                case "Level3":
                    FindObjectOfType<LevelTransition>().LoadNextLevel("Level4");
                    break;
                case "Level4":
                    FindObjectOfType<LevelTransition>().LoadNextLevel("Home");
                    break;
            }
        }
    }
}
