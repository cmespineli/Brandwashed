using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Game : MonoBehaviour
{
    public static GameManager_Game instance;

    public Transform cardGrid;
    public GameObject letterButtonPrefab;
    public Transform buttonPanel;
    public RiddleManager riddleManager;

    private Card firstCard, secondCard;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        riddleManager.LoadNewRiddle();
    }

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

    public void CreateLetterButton(string letter)
    {
        GameObject newButton = Instantiate(letterButtonPrefab, buttonPanel);
        TMP_Text textComponent = newButton.GetComponentInChildren<TMP_Text>();
        textComponent.text = letter;

        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => WordInput_Game.instance.AddLetter(letter));
    }

    public void SetupCards(string word)
    {
        List<char> letters = new List<char>(word.ToUpper());
        letters.AddRange(letters);
        letters = letters.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < cardGrid.childCount; i++)
        {
            Card card = cardGrid.GetChild(i).GetComponent<Card>();
            card.letter = letters[i].ToString();

            if (card.possibleSprites != null && card.possibleSprites.Length > 0)
            {
                int index = Random.Range(0, card.possibleSprites.Length);
                card.frontImage.sprite = card.possibleSprites[index];
            }

            card.HideCard();
        }
    }
}
