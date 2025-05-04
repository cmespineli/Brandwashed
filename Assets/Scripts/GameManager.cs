using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform cardGrid;
    public List<Card> cards = new List<Card>();
    public List<char> matchedLetters = new List<char>();

    private Card firstCard;
    private Card secondCard;

    private int totalMatches;
    private int matchesFound;
    private int step3MatchesFound;

    private bool hasMatchedFirstPair = false;
    private bool matchingDisabled = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        AssignLettersToExistingCards();
    }

    void AssignLettersToExistingCards()
    {
        string letters = "CARDSCARDS";
        List<char> charList = new List<char>(letters.ToCharArray());

        for (int i = 0; i < charList.Count; i++)
        {
            int rnd = Random.Range(i, charList.Count);
            char temp = charList[i];
            charList[i] = charList[rnd];
            charList[rnd] = temp;
        }

        cards.Clear();
        matchedLetters.Clear();
        int index = 0;
        foreach (Transform child in cardGrid)
        {
            Card card = child.GetComponent<Card>();
            if (card != null && index < charList.Count)
            {
                char letter = charList[index++];
                card.AssignLetter(letter.ToString());
                card.gameObject.SetActive(true);
                card.Unlock();
                cards.Add(card);
            }
        }

        totalMatches = cards.Count / 2;
        matchesFound = 0;
        step3MatchesFound = 0;
        hasMatchedFirstPair = false;
        matchingDisabled = false;
    }

    public void CardRevealed(Card card)
    {
        if (matchingDisabled || firstCard == card) return;

        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstCard.GetLetter() == secondCard.GetLetter())
        {
            firstCard.Lock();
            secondCard.Lock();
            matchesFound++;

            char matchedChar = firstCard.GetLetter();
            if (!matchedLetters.Contains(matchedChar))
            {
                matchedLetters.Add(matchedChar);
            }

            if (TutorialManager.instance != null)
            {
                if (!hasMatchedFirstPair && TutorialManager.instance.CurrentStepIs(2))
                {
                    hasMatchedFirstPair = true;
                    matchingDisabled = true;

                    foreach (Card card in cards)
                    {
                        if (card != firstCard && card != secondCard)
                        {
                            card.Lock();
                        }
                    }

                    TutorialManager.instance.ShowGoodJobThenNext("Good job!");
                }
                else if (TutorialManager.instance.CurrentStepIs(3))
                {
                    step3MatchesFound++;

                    if (step3MatchesFound == totalMatches - 1)
                    {
                        TutorialManager.instance.ShowGoodJobThenNext("Great work!");
                    }
                }
            }
        }
        else
        {
            firstCard.ResetCard();
            secondCard.ResetCard();
        }

        firstCard = null;
        secondCard = null;
    }

    public void EnableAllUnlockedCards()
    {
        foreach (Card card in cards)
        {
            card.Unlock();
            card.ResetCard();
        }

        matchingDisabled = false;
    }
}
