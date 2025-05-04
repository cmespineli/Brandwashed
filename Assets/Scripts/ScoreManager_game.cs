// ScoreManager_Game.cs
using UnityEngine;
using TMPro;

public class ScoreManager_Game : MonoBehaviour
{
    public static ScoreManager_Game instance;

    public TMP_Text scoreText;
    private int score = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
} 
