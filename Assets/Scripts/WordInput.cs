using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordInput : MonoBehaviour
{
    public static WordInput instance;

    public List<TMP_Text> inputSlots;   // Drag in your 5 TMP text boxes here
    private int currentIndex = 0;

    public string correctWord;          // Set this in the Inspector (e.g., "APPLE")
    public TMP_Text feedback;           // Feedback box for messages

    void Awake()
    {
        instance = this;
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

        if (attempt.ToUpper() == correctWord.ToUpper())
        {
            feedback.text = "✅ Correct!";
        }
        else
        {
            feedback.text = "❌ Try Again!";
        }
    }

    public void ResetInput()
    {
        foreach (TMP_Text t in inputSlots)
        {
            t.text = "";
        }
        currentIndex = 0;
        feedback.text = "";
    }
}
