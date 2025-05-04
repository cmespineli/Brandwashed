using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class WordInput : MonoBehaviour
{
    public static WordInput instance;
    public TMP_Text[] letterSlots;
    public Button[] letterButtons;
    public GameObject feedbackText;
    private string correctAnswer = "CARDS";
    private List<string> currentInput = new List<string>();

    private void Awake()
    {
        instance = this;
    }

    public void AddLetter(string letter)
    {
        if (currentInput.Count < letterSlots.Length)
        {
            currentInput.Add(letter);
            UpdateLetterSlots();
        }
    }

    public void DeleteLastLetter()
    {
        if (currentInput.Count > 0)
        {
            currentInput.RemoveAt(currentInput.Count - 1);
            UpdateLetterSlots();
        }
    }

    public void ClearInput()
    {
        currentInput.Clear();
        UpdateLetterSlots();
    }

    void UpdateLetterSlots()
    {
        for (int i = 0; i < letterSlots.Length; i++)
        {
            letterSlots[i].text = i < currentInput.Count ? currentInput[i] : "";
        }
    }

    public void SubmitAnswer()
    {
        string attempt = string.Join("", currentInput);
        if (attempt == correctAnswer)
        {
            TutorialManager.instance.ShowGoodJobThenNext("Good job!");
        }
        else
        {
            TutorialManager.instance.ShowGoodJobThenNext("Try again!");
        }
    }

    public void EnableLetterButtons(List<string> letters)
    {
        List<string> randomized = letters.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < letterButtons.Length; i++)
        {
            if (i < randomized.Count)
            {
                letterButtons[i].gameObject.SetActive(true);
                letterButtons[i].GetComponentInChildren<TMP_Text>().text = randomized[i];
                string letter = randomized[i];
                letterButtons[i].onClick.RemoveAllListeners();
                letterButtons[i].onClick.AddListener(() => AddLetter(letter));
            }
            else
            {
                letterButtons[i].gameObject.SetActive(false);
            }
        }
    }
}