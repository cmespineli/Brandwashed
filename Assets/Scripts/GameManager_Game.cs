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
    }

    public void SetupCards(string word)
    {
        allCards.Clear();

        // Get all cards from grid
        foreach (Transform child in cardGrid)
        {
            Card_Game card = child.GetComponent<Card_Game>();
            if (card != null)
            {
                allCards.Add(card);
            }
        }

        // Create letter pairs and shuffle
        List<char> letters = new List<char>(word.ToUpper());
        letters.AddRange(letters);
        letters = letters.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < allCards.Count; i++)
        {
            allCards[i].AssignLetter(letters[i].ToString());
            allCards[i].HideCard();
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
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);
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

        // Assign the letter and setup the button
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
}
