using UnityEngine;
using UnityEngine.UI;   // only if you’re using a UI Text

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public Text scoreText; 

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
