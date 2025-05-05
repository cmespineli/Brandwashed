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
    public GameObject wrongAnswerSprite;
    public AudioSource buzzerAudio;
    public GameObject checkmarkSprite;
    public AudioSource successAudio;
    public float transitionDelay = 1.5f; // delay before starting next round


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
            ScoreManager_Game.instance.AddPoints(500);

            // ✅ Add 30 seconds to timer
            TimerManager.instance.AddTime(30f);

            // ✅ Show checkmark
            if (checkmarkSprite != null)
                checkmarkSprite.SetActive(true);

            // ✅ Play success sound
            if (successAudio != null)
                successAudio.Play();

            // ✅ Wait briefly to show feedback
            yield return new WaitForSeconds(transitionDelay);

            // Hide checkmark again (optional)
            if (checkmarkSprite != null)
                checkmarkSprite.SetActive(false);

            // ✅ Hide riddle UI
            gameObject.SetActive(false);
            GameManager_Game.instance.buttonPanel.gameObject.SetActive(false);
            GameManager_Game.instance.riddleManager.riddleText.gameObject.SetActive(false);
            inputControlsPanel.SetActive(false);

            // ✅ Proceed to next card-matching round
            GameManager_Game.instance.cardGrid.gameObject.SetActive(true);
            GameManager_Game.instance.StartNextRound();
        }
        else
        {
            feedbackText.text = "REDACTED.";

            // ❌ Show wrong answer sprite
            if (wrongAnswerSprite != null)
                wrongAnswerSprite.SetActive(true);

            // 🔊 Play buzzer sound
            if (buzzerAudio != null)
                buzzerAudio.Play();

            HPManager_Game.instance.TakeDamage();

            yield return new WaitForSeconds(2f);

            // ❌ Hide the wrong answer sprite
            if (wrongAnswerSprite != null)
                wrongAnswerSprite.SetActive(false);

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
