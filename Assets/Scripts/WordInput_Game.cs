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
        // Safety checks
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

        inputSlots[currentIndex].text = letter.Trim();  // Trim in case there's a space
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
            TimerManager.instance.ResetTimer(); // Reset timer on correct word
        }
        else
        {
            feedbackText.text = "❌ Try Again!";
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
