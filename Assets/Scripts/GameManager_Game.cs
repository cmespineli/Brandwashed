using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Game : MonoBehaviour
{
    public static GameManager_Game instance;

    [Header("References")]
    public Transform cardGrid;
    public GameObject letterButtonPrefab;
    public Transform buttonPanel;
    public RiddleManager riddleManager;
    public ScoreManager_Game scoreManager;

    [Header("Gameplay")]
    private List<Card_Game> allCards = new List<Card_Game>();
    private Card_Game firstCard, secondCard;
    private int matchCount = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        riddleManager.LoadNewRiddle();
        riddleManager.StartCardPhase();
    }

    public void SetupCards(string word)
    {
        allCards.Clear();

        // Collect cards from grid children
        foreach (Transform child in cardGrid)
        {
            Card_Game card = child.GetComponent<Card_Game>();
            if (card != null)
            {
                card.gameObject.SetActive(true); // Ensure reactivated
                allCards.Add(card);
            }
        }

        // Shuffle and assign 5 letter pairs to the cards
        List<char> letters = new List<char>(word.ToUpper());
        letters.AddRange(letters); // create pairs
        letters = letters.OrderBy(x => Random.value).ToList(); // shuffle

        for (int i = 0; i < allCards.Count; i++)
        {
            if (i < letters.Count)
            {
                allCards[i].AssignLetter(letters[i].ToString());
                allCards[i].HideCard();
                allCards[i].gameObject.SetActive(true);
            }
            else
            {
                // Hide extra cards if you have more than 10 in the grid
                allCards[i].gameObject.SetActive(false);
            }
        }

        matchCount = 0;
    }

    public void CardRevealed(Card_Game card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null && card != firstCard)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstCard.letter == secondCard.letter && !string.IsNullOrEmpty(firstCard.letter))
        {
            scoreManager.AddPoints(100);
            CreateLetterButton(firstCard.letter);
            firstCard.gameObject.SetActive(false);
            secondCard.gameObject.SetActive(false);
            matchCount++;

            if (matchCount >= 5)
            {
                yield return new WaitForSeconds(0.5f);
                riddleManager.ShowRiddleUI();
            }
        }
        else
        {
            firstCard.HideCard();
            secondCard.HideCard();
        }

        firstCard = null;
        secondCard = null;
    }

    public void CreateLetterButton(string letter)
    {
        GameObject newButton = Instantiate(letterButtonPrefab, buttonPanel);

        LetterButtonHandler handler = newButton.GetComponent<LetterButtonHandler>();
        if (handler != null)
        {
            handler.SetLetter(letter);
        }
        else
        {
            Debug.LogWarning("LetterButton prefab is missing LetterButtonHandler!");
        }
    }

    public void StartNextRound()
    {
        foreach (Transform child in buttonPanel)
        {
            Destroy(child.gameObject);
        }

        riddleManager.LoadNewRiddle();
        cardGrid.gameObject.SetActive(true);
        riddleManager.StartCardPhase();
    }
}
