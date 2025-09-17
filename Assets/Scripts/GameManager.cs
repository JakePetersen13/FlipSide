using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int targetScore = 3;
    public TextMeshProUGUI scoreText;

    public PhysicsBase PhysicsBase;
    public GameObject RedPlatforms;
    public GameObject BluePlatforms;

    Color32 lightBlueOpaque = new Color32(0, 62, 255, 255);
    Color32 lightRedOpaque = new Color32(255, 0, 4, 255);

    Color32 lightBlueTransparent = new Color32(0, 62, 255, 20);
    Color32 lightRedTransparent = new Color32(255, 0, 4, 20);

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
                    FindObjectOfType<LevelTransition>().LoadNextLevel("Win");
                    break;
            }
        }
    }

    void Update()
    {
        if (PhysicsBase.gravityDirection == 1)
        {
            BluePlatforms.GetComponent<Tilemap>().color = lightBlueOpaque;
            RedPlatforms.GetComponent<Tilemap>().color = lightRedTransparent;
        }
        if (PhysicsBase.gravityDirection == -1)
        {
            BluePlatforms.GetComponent<Tilemap>().color = lightBlueTransparent;
            RedPlatforms.GetComponent<Tilemap>().color = lightRedOpaque;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<LevelTransition>().LoadNextLevel(SceneManager.GetActiveScene().name);
        }
    }
}
