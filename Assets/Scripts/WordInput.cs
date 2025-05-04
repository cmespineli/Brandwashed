using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class WordInput : MonoBehaviour
{
    public static WordInput instance;

    public List<Button> letterButtons;
    public TMP_Text resultText;
    private string inputWord = "";
    private string correctAnswer = "CARDS";

    void Awake()
    {
        instance = this;
    }

    public void AddLetter(string letter)
    {
        if (inputWord.Length < 5)
        {
            inputWord += letter;
            UpdateResultText();
        }
    }

    public void DeleteLetter()
    {
        if (inputWord.Length > 0)
        {
            inputWord = inputWord.Substring(0, inputWord.Length - 1);
            UpdateResultText();
        }
    }

    public void ClearInput()
    {
        inputWord = "";
        UpdateResultText();
    }

    public void SubmitWord()
    {
        if (inputWord == correctAnswer)
        {
            TutorialManager.instance.ShowPraiseThenNext("Good job!");
        }
        else
        {
            TutorialManager.instance.ShowPraiseThenNext("Try again!");
            ClearInput();
        }
    }

    public void UpdateResultText()
    {
        resultText.text = inputWord;
    }

    public void EnableLetterButtons(List<string> letters)
    {
        letters = letters.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < letterButtons.Count; i++)
        {
            if (i < letters.Count)
            {
                string l = letters[i];
                letterButtons[i].GetComponentInChildren<TMP_Text>().text = l;
                letterButtons[i].onClick.RemoveAllListeners();
                letterButtons[i].onClick.AddListener(() => AddLetter(l));
                letterButtons[i].gameObject.SetActive(true);
            }
            else
            {
                letterButtons[i].gameObject.SetActive(false);
            }
        }
    }
}