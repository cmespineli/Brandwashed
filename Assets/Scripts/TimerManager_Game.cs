using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;

    public float maxTime = 120f;
    private float currentTime;
    public TMP_Text timerText;
    private bool timerActive = true;
    public GameObject timesUpImage;
    public float delayBeforeGameOver = 2f;
    public AudioSource timesUpAudio;


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

    public void AddTime(float seconds)
    {
        currentTime += seconds;
        UpdateTimerUI();
    }

    void GameOver()
    {
        Debug.Log("Time's up! Game Over");

        if (timesUpImage != null)
            timesUpImage.SetActive(true);

        if (timesUpAudio != null)
            timesUpAudio.Play();

        GameManager_Game.instance.SaveGameStatsBeforeEnd();

        StartCoroutine(DelayedGameOver());
    }

    public float GetElapsedTime()
    {
        return maxTime - currentTime;
    }



    IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(delayBeforeGameOver);
        SceneManager.LoadScene("07EndScreen");
    }
}
