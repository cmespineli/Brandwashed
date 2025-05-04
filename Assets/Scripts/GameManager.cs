// =====================
// GameManager.cs
// =====================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public RectTransform cardGrid;
    private List<Card> allCards = new List<Card>();
    private List<Card> revealedCards = new List<Card>();

    private char[] letterPool = { 'C', 'A', 'R', 'D', 'S', 'C', 'A', 'R', 'D', 'S' };
    private HashSet<char> matchedLetters = new HashSet<char>();

    void Awake()
    {
        instance = this;
        InitializeCards();
    }

    void InitializeCards()
    {
        allCards = cardGrid.GetComponentsInChildren<Card>().ToList();
        var shuffled = letterPool.OrderBy(x => Random.value).ToArray();

        for (int i = 0; i < allCards.Count; i++)
        {
            allCards[i].SetLetter(shuffled[i]);
            allCards[i].SetInteractable(false);
        }
    }

    public void EnableAllUnlockedCards()
    {
        foreach (Card card in allCards)
        {
            card.SetInteractable(true);
        }
    }

    public void OnCardRevealed(Card card)
    {
        if (revealedCards.Contains(card)) return;

        revealedCards.Add(card);

        if (revealedCards.Count == 2)
            StartCoroutine(CheckMatch());
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        Card card1 = revealedCards[0];
        Card card2 = revealedCards[1];

        if (card1.GetLetter() == card2.GetLetter())
        {
            card1.Lock();
            card2.Lock();
            matchedLetters.Add(card1.GetLetter());

            if (TutorialManager.instance.CurrentStepIs(2))
            {
                foreach (Card card in allCards)
                {
                    if (!card.IsLocked()) card.SetInteractable(false);
                }
                TutorialManager.instance.ShowPraiseThenNext("Good job!");
            }
            else if (TutorialManager.instance.CurrentStepIs(3))
            {
                bool allMatched = allCards.All(c => c.IsLocked());
                if (allMatched)
                {
                    TutorialManager.instance.ShowPraiseThenNext("Great work!");
                }
            }
        }
        else
        {
            card1.HideLetter();
            card2.HideLetter();
        }

        revealedCards.Clear();

        if (matchedLetters.Count >= 5 && TutorialManager.instance.CurrentStepIs(3))
        {
            TutorialManager.instance.AdvanceToRiddleStage(matchedLetters.Select(c => c.ToString()).ToList());
        }
    }

    public List<char> GetMatchedLetters()
    {
        return matchedLetters.ToList();
    }
}