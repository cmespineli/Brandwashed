// =====================
// TutorialManager.cs
// =====================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public TMP_Text tutorialText;
    public TMP_Text tutorialTextRect;
    public Button nextButton;
    public GameObject cardGrid;
    public GameObject wordInputPanel;
    public GameObject buttonPanel;
    public GameObject riddleText;
    public GameObject successIcon;
    public GameObject inputControlsPanel;

    private int step = 0;

    private string[] messages = new string[]
    {
        "Welcome to Brandwashed!",
        "Your objective is to use the cards on the table and match them to help solve the riddle!",
        "Match two cards of the same letter!",
        "Now match all the pairs!",
        "Use the letter buttons to spell the answer!",
        "Are you ready to try it for real?"
    };

    void Awake()
    {
        instance = this;
    }

    public void StartTutorial()
    {
        step = 0;
        cardGrid.SetActive(false);
        wordInputPanel.SetActive(false);
        buttonPanel.SetActive(false);
        riddleText.SetActive(false);
        inputControlsPanel.SetActive(false);
        tutorialText.gameObject.SetActive(false);
        tutorialTextRect.gameObject.SetActive(false);
        ShowStep();
    }

    public void NextStep()
    {
        step++;
        nextButton.gameObject.SetActive(false);

        if (step < messages.Length)
        {
            ShowStep();
        }
        else
        {
            SceneManager.LoadScene("RealGameScene");
        }
    }

    public void AdvanceToRiddleStage(List<string> matchedLetters)
    {
        step = 4;
        ShowStep(matchedLetters);
    }

    public void ShowStep(List<string> matchedLetters = null)
    {
        tutorialText.text = "";
        tutorialTextRect.text = "";

        cardGrid.SetActive(false);
        wordInputPanel.SetActive(false);
        buttonPanel.SetActive(false);
        riddleText.SetActive(false);
        inputControlsPanel.SetActive(false);
        nextButton.gameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);
        tutorialTextRect.gameObject.SetActive(false);

        if (step == 0 || step == 1)
        {
            tutorialTextRect.gameObject.SetActive(true);
            tutorialTextRect.text = messages[step];
            nextButton.gameObject.SetActive(true);
        }
        else if (step == 2)
        {
            tutorialText.gameObject.SetActive(true);
            tutorialText.text = messages[step];
            cardGrid.SetActive(true);
            GameManager.instance.EnableAllUnlockedCards();
        }
        else if (step == 3)
        {
            tutorialText.gameObject.SetActive(true);
            tutorialText.text = messages[step];
            cardGrid.SetActive(true);
            GameManager.instance.EnableAllUnlockedCards();
        }
        else if (step == 4)
        {
            tutorialText.gameObject.SetActive(true);
            tutorialText.text = messages[step];
            wordInputPanel.SetActive(true);
            buttonPanel.SetActive(true);
            riddleText.SetActive(true);
            inputControlsPanel.SetActive(true);
            riddleText.GetComponent<TMP_Text>().text = "I come in pairs but never walk, I spell solutions but never talk. What am I?";
            WordInput.instance.EnableLetterButtons(matchedLetters);
        }
        else if (step == 5)
        {
            tutorialTextRect.gameObject.SetActive(true);
            tutorialTextRect.text = messages[step];
            nextButton.gameObject.SetActive(true);
        }
    }

    public void ShowPraiseThenNext(string message)
    {
        StartCoroutine(PraiseRoutine(message));
    }

    IEnumerator PraiseRoutine(string message)
    {
        tutorialText.text = message;
        yield return new WaitForSeconds(1.2f);
        ShowNextButton();
    }

    public void ShowNextButton()
    {
        nextButton.gameObject.SetActive(true);
    }

    public bool CurrentStepIs(int i)
    {
        return step == i;
    }

    public int GetCurrentStep()
    {
        return step;
    }
}