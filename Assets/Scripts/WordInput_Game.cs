// WordInput_Game.cs
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordInput_Game : MonoBehaviour
{
    public static WordInput_Game instance;

    public List<TMP_Text> inputSlots;
    public TMP_Text feedbackText;
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
        if (currentIndex >= inputSlots.Count) return;

        inputSlots[currentIndex].text = letter;
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
            TimerManager.instance.ResetTimer(); // Reset timer on correct answer
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
            t.text = "";
        }
        currentIndex = 0;
        feedbackText.text = "";
    }
} 
