using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform cardGrid;
    private List<Card> allCards = new List<Card>();

    private Card firstCard, secondCard;
    private int matchCount = 0;
    private HashSet<char> matchedLetters = new HashSet<char>();

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip flipSound;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitializeCards();
    }

    void InitializeCards()
    {
        allCards.Clear();

        foreach (Transform child in cardGrid)
        {
            Card card = child.GetComponent<Card>();
            if (card != null)
            {
                allCards.Add(card);
                card.HideLetter();
                card.SetInteractable(false);
            }
        }

        List<char> letters = new List<char>("CARDSCARDS");
        System.Random rng = new System.Random();
        letters = letters.OrderBy(_ => rng.Next()).ToList();

        for (int i = 0; i < allCards.Count; i++)
        {
            allCards[i].AssignLetter(letters[i].ToString());
        }
    }

    public void StartStep3Matching()
    {
        matchCount = 0;
        firstCard = secondCard = null;

        foreach (Card card in allCards)
        {
            card.HideLetter();
            card.SetInteractable(true);
        }
    }

    public void CardRevealed(Card card)
    {
        PlayFlipSound();

        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null && card != firstCard)
        {
            secondCard = card;
            StartCoroutine(EvaluateMatch());
        }
    }

    IEnumerator EvaluateMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstCard.GetLetter() == secondCard.GetLetter())
        {
            matchedLetters.Add(firstCard.GetLetter()[0]);
            firstCard.SetInteractable(false);
            secondCard.SetInteractable(false);
            matchCount++;

            if (TutorialManager.instance.CurrentStepIs(2))
            {
                TutorialManager.instance.ShowGoodJobThenNext("Good job!");
            }
            else if (TutorialManager.instance.CurrentStepIs(3) && matchCount >= 5)
            {
                TutorialManager.instance.ShowGoodJobThenNext("Great job!");
            }
        }
        else
        {
            firstCard.HideLetter();
            secondCard.HideLetter();
        }

        firstCard = secondCard = null;
    }

    void PlayFlipSound()
    {
        if (audioSource != null && flipSound != null)
        {
            audioSource.PlayOneShot(flipSound);
        }
    }

    public HashSet<char> GetMatchedLetters()
    {
        return matchedLetters;
    }
}
