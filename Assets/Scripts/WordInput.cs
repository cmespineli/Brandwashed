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
    public GameObject letterButtonPrefab;  // assign in Inspector
    public Transform buttonPanel;   
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
        // Clear existing buttons
        foreach (Transform child in buttonPanel)
        {
            Destroy(child.gameObject);
        }

        // Shuffle letters
        letters = letters.OrderBy(x => Random.value).ToList();

        foreach (string letter in letters)
        {
            GameObject newButton = Instantiate(letterButtonPrefab, buttonPanel);
            newButton.GetComponent<LetterButtonHandler>().SetLetter(letter);
        }
    }
}