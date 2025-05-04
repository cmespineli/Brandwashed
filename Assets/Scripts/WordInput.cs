using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WordInput : MonoBehaviour
{
    public static WordInput instance;

    public TMP_Text[] letterSlots;
    public TMP_Text feedbackText;

    private int currentIndex = 0;

    void OnEnable()
    {
        instance = this;
    }

    public void AddLetter(string letter)
    {
        if (currentIndex < letterSlots.Length)
        {
            letterSlots[currentIndex].text = letter;
            currentIndex++;
        }
    }

    public void DeleteLastLetter()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            letterSlots[currentIndex].text = "";
        }
    }

    public void ClearInput()
    {
        for (int i = 0; i < letterSlots.Length; i++)
        {
            letterSlots[i].text = "";
        }
        currentIndex = 0;
    }

    public void CheckAnswer()
    {
        string input = "";
        foreach (TMP_Text letter in letterSlots)
        {
            input += letter.text.ToUpper();
        }

        if (input == "CARDS")
        {
            feedbackText.text = "Correct!";
        }
        else
        {
            feedbackText.text = "Try again!";
        }
    }

    public void EnableLetterButtons(List<char> letters)
    {
        if (letters == null)
        {
            Debug.LogWarning("EnableLetterButtons called with null letter list.");
            return;
        }

        foreach (Transform button in transform)
        {
            Button b = button.GetComponent<Button>();
            TMP_Text text = b.GetComponentInChildren<TMP_Text>();

            if (text != null && letters.Contains(text.text[0]))
            {
                b.interactable = true;
            }
            else
            {
                b.interactable = false;
            }
        }
    }
}
