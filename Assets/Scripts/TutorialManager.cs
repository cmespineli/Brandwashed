using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text tutorialText;
    public Button nextButton;
    public GameObject cardGrid;
    public GameObject wordInputPanel;
    public GameObject buttonPanel;

    private int step = 0;

    private string[] messages = new string[]
    {
        "Match two cards to reveal a letter!",
        "Keep matching cards until they're all gone!",
        "Use the letter buttons to guess the word!"
    };

    public static TutorialManager instance;

    void Awake()
    {
        instance = this;
    }

    public void StartTutorial()
    {
        step = 0;
        ShowStep();
    }

    public void NextStep()
    {
        step++;
        nextButton.gameObject.SetActive(false); // hide after advancing

        if (step < messages.Length)
            ShowStep();
        else
            EndTutorial();
    }

    void ShowStep()
    {
        tutorialText.text = messages[step];
        nextButton.gameObject.SetActive(false); // always hide initially

        if (step == 0)
        {
            cardGrid.SetActive(true);
            wordInputPanel.SetActive(false);
            buttonPanel.SetActive(false);
        }
        else if (step == 1)
        {
            cardGrid.SetActive(true);
            wordInputPanel.SetActive(false);
            buttonPanel.SetActive(false);
        }
        else if (step == 2)
        {
            cardGrid.SetActive(false);
            wordInputPanel.SetActive(true);
            buttonPanel.SetActive(true);
        }
    }

    public void ShowNextButton()
    {
        nextButton.gameObject.SetActive(true);
    }

    public int GetCurrentStep()
    {
        return step;
    }

    public bool CurrentStepIs(int index)
    {
        return step == index;
    }

    void EndTutorial()
    {
        tutorialText.text = "";
        nextButton.gameObject.SetActive(false);
    }
}
