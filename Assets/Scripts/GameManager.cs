using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Card Matching")]
    public GameObject cardPrefab;              // Optional: used if you're spawning cards
    public Transform cardGrid;                 // The GridLayout that holds cards

    [Header("Letter Button Creation")]
    public GameObject letterButtonPrefab;      // The prefab for letter buttons
    public Transform buttonPanel;              // The panel where letter buttons are placed

    [Header("Tutorial")]
    public TutorialManager tutorialManager;    // Reference to tutorial system

    private Card firstCard, secondCard;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tutorialManager.StartTutorial(); // This line is tutorial-specific
    }

    // Called when a card is revealed
    public void CardRevealed(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
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
        }
        else
        {
            firstCard.HideCard();
            secondCard.HideCard();
        }

        firstCard = null;
        secondCard = null;
    }

    // Creates a letter button that players can use to guess the riddle
    void CreateLetterButton(string letter)
    {
        GameObject newButton = Instantiate(letterButtonPrefab, buttonPanel);
        TMP_Text textComponent = newButton.GetComponentInChildren<TMP_Text>();
        textComponent.text = letter;

        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => WordInput.instance.AddLetter(letter));
    }
}
