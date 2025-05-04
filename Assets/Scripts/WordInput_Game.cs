using System.Collections;
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
    private bool isChecking = false;

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
        if (currentIndex >= inputSlots.Count || isChecking) return;

        if (inputSlots[currentIndex] != null)
        {
            inputSlots[currentIndex].text = letter.Trim();
            currentIndex++;
        }
    }

    public void SubmitWord()
    {
        if (currentIndex < inputSlots.Count || isChecking) return;

        StartCoroutine(CheckAnswerRoutine());
    }

    IEnumerator CheckAnswerRoutine()
    {
        isChecking = true;

        string attempt = "";
        foreach (TMP_Text t in inputSlots)
        {
            attempt += t.text;
        }

        if (attempt.ToUpper() == correctWord)
        {
            feedbackText.text = "CORRECT.";
            ScoreManager_Game.instance.AddPoints(500); // ✅ Add riddle points
            yield return new WaitForSeconds(2f);

            // Hide riddle UI
            gameObject.SetActive(false);
            GameManager_Game.instance.buttonPanel.gameObject.SetActive(false);
            GameManager_Game.instance.riddleManager.riddleText.gameObject.SetActive(false);
            inputControlsPanel.SetActive(false);

            // Show cards and move on
            GameManager_Game.instance.cardGrid.gameObject.SetActive(true);
            GameManager_Game.instance.StartNextRound();
        }
        else
        {
            feedbackText.text = "REDACTED.";
            HPManager_Game.instance.TakeDamage();
            yield return new WaitForSeconds(2f);
            feedbackText.text = "";
        }

        isChecking = false;
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
        if (currentIndex > 0 && !isChecking)
        {
            currentIndex--;
            inputSlots[currentIndex].text = "";
        }
    }

    public void ClearAll()
    {
        ResetInput();
    }
}
