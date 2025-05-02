using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform cardGrid;
    public GameObject letterButtonPrefab;
    public Transform buttonPanel;

    private Card firstCard, secondCard;
    private bool canReveal = true;

    void Awake()
    {
        instance = this;
    }

    public void CardRevealed(Card card)
    {
        if (!canReveal) return;

        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            canReveal = false;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstCard.letter == secondCard.letter)
        {
            CreateLetterButton(firstCard.letter);
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);

            // ✅ Step 0: Show Next after first match
            if (TutorialManager.instance != null && TutorialManager.instance.CurrentStepIs(0))
            {
                TutorialManager.instance.ShowNextButton();
            }

            // ✅ Step 1: Show Next after all cards are matched
            if (TutorialManager.instance != null && TutorialManager.instance.CurrentStepIs(1))
            {
                if (cardGrid.childCount <= 2) // only these 2 cards left
                {
                    TutorialManager.instance.ShowNextButton();
                }
            }
        }
        else
        {
            firstCard.HideCard();
            secondCard.HideCard();
        }

        firstCard = null;
        secondCard = null;
        canReveal = true;
    }

    void CreateLetterButton(string letter)
    {
        GameObject newButton = Instantiate(letterButtonPrefab, buttonPanel);
        TMP_Text textComponent = newButton.GetComponentInChildren<TMP_Text>();
        textComponent.text = letter;

        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => WordInput.instance.AddLetter(letter));
    }
}
