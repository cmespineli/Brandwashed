using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        "Each matched pair gives you a usable letter.",
        "Use those letters to fill in the riddle answer!",
        "Click letter buttons to guess the word!"
    };

    public void StartTutorial()
    {
        step = 0;
        ShowStep();
    }

    public void NextStep()
    {
        step++;
        if (step < messages.Length)
            ShowStep();
        else
            EndTutorial();
    }

    void ShowStep()
    {
        tutorialText.text = messages[step];

        // Example logic to switch UI views based on tutorial step
        if (step == 0)
        {
            cardGrid.SetActive(true);
            wordInputPanel.SetActive(false);
            buttonPanel.SetActive(false);
        }
        else if (step == 2)
        {
            cardGrid.SetActive(false);
            buttonPanel.SetActive(true);
            wordInputPanel.SetActive(true);
        }

        nextButton.gameObject.SetActive(true); // ✅ FIXED!
    }

    void EndTutorial()
    {
        tutorialText.text = "";
        nextButton.gameObject.SetActive(false); // ✅ Also good
    }
}
