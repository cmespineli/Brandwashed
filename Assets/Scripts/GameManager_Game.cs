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

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip flipSound;

    [Header("UI")]
    public TMP_Text roundText;
    private int roundNumber = 1;

    [Header("Gameplay")]
    private List<Card_Game> allCards = new List<Card_Game>();
    private Card_Game firstCard, secondCard;
    private int matchCount = 0;
    private bool canClick = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateRoundUI();
        riddleManager.LoadNewRiddle();
        riddleManager.StartCardPhase();
    }

    public void SetupCards(string word)
    {
        allCards.Clear();

        foreach (Transform child in cardGrid)
        {
            Card_Game card = child.GetComponent<Card_Game>();
            if (card != null)
            {
                card.gameObject.SetActive(true);
                allCards.Add(card);
            }
        }

        List<char> letters = new List<char>(word.ToUpper());
        letters.AddRange(letters);
        letters = letters.OrderBy(x => Random.value).ToList();

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
                allCards[i].gameObject.SetActive(false);
            }
        }

        matchCount = 0;
    }

    public void CardRevealed(Card_Game card)
    {
        if (!canClick) return;

        if (firstCard == null)
        {
            firstCard = card;
            PlayFlipSound();
        }
        else if (secondCard == null && card != firstCard)
        {
            secondCard = card;
            PlayFlipSound();
            canClick = false;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.25f);

        if (firstCard.letter == secondCard.letter && !string.IsNullOrEmpty(firstCard.letter))
        {
            scoreManager.AddPoints(100);
            CreateLetterButton(firstCard.letter);
            firstCard.gameObject.SetActive(false);
            secondCard.gameObject.SetActive(false);
            matchCount++;

            if (matchCount >= 5)
            {
                yield return new WaitForSeconds(0.25f);
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
        canClick = true;
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

        roundNumber++;
        UpdateRoundUI();

        riddleManager.LoadNewRiddle();
        cardGrid.gameObject.SetActive(true);
        riddleManager.StartCardPhase();
    }

    void UpdateRoundUI()
    {
        if (roundText != null)
        {
            roundText.text = $"Round: {roundNumber}";
        }
    }

    void PlayFlipSound()
    {
        if (audioSource != null && flipSound != null)
        {
            audioSource.PlayOneShot(flipSound);
        }
    }
}
