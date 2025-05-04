using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordInput_Game : MonoBehaviour
{
    public static WordInput_Game instance;

    [Header("Input UI")]
    public List<TMP_Text> inputSlots;
    public TMP_Text feedbackText;
    public GameObject inputControlsPanel;

    [Header("Word Logic")]
    public string correctWord;
    private int currentIndex = 0;

    void Awake()
    {
        instance = this;
    }

    public void SetCorrectWord(string word)
    {
        correctWord = word.ToUpper();
        ResetInput();
    }

    public void AddLetter(string letter)
    {
        if (currentIndex >= inputSlots.Count)
        {
            Debug.LogWarning("Attempted to add letter beyond slot count.");
            return;
        }

        if (inputSlots[currentIndex] == null)
        {
            Debug.LogError("Input slot " + currentIndex + " is null!");
            return;
        }

        inputSlots[currentIndex].text = letter.Trim();
        currentIndex++;

        if (currentIndex == inputSlots.Count)
        {
            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        string attempt = "";
        foreach (TMP_Text t in inputSlots)
        {
            attempt += t.text;
        }

        if (attempt.ToUpper() == correctWord)
        {
            feedbackText.text = "✅ Correct!";
            TimerManager.instance.ResetTimer();

            // Hide riddle-related UI
            gameObject.SetActive(false); // WordInput_Game
            GameManager_Game.instance.buttonPanel.gameObject.SetActive(false);
            GameManager_Game.instance.riddleManager.riddleText.gameObject.SetActive(false); // Hide riddle text
            inputControlsPanel.SetActive(false);

            // Show cards again
            GameManager_Game.instance.cardGrid.gameObject.SetActive(true);

            // Start next round
            GameManager_Game.instance.StartNextRound();
        }
        else
        {
            feedbackText.text = "❌ Try Again!";
            HPManager_Game.instance.TakeDamage();
        }
    }

    public void ResetInput()
    {
        foreach (TMP_Text t in inputSlots)
        {
            if (t != null)
                t.text = "";
        }
        currentIndex = 0;
        feedbackText.text = "";
    }

    public void DeleteLetter()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            inputSlots[currentIndex].text = "";
        }
    }

    public void ClearAll()
    {
        ResetInput();
    }

    public void SubmitWord()
    {
        CheckAnswer();
    }
}
