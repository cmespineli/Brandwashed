// TimerManager.cs
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;

    public float maxTime = 120f;
    private float currentTime;
    public TMP_Text timerText;
    private bool timerActive = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentTime = maxTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!timerActive) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            timerActive = false;
            timerText.text = "0";
            GameOver();
        }
        else
        {
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }

    public void ResetTimer()
    {
        currentTime = maxTime;
        timerActive = true;
        UpdateTimerUI();
    }

    void GameOver()
    {
        Debug.Log("Time's up! Game Over.");
        // Add your game over handling here
    }
} 
