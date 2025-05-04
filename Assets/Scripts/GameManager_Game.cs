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

    public RiddleManager riddleManager;         // Set in Inspector
    public ScoreManager_Game scoreManager;      // Set in Inspector

    private Card_Game firstCard, secondCard;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        riddleManager.LoadNewRiddle();
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
        yield return new WaitForSeconds(1f);

        if (firstCard.letter == secondCard.letter)
        {
            // Add score
            scoreManager?.AddPoints(100);

            // Disable the matched buttons
            firstCard.button.interactable = false;
            secondCard.button.interactable = false;

            // Create letter input button
            CreateLetterButton(firstCard.letter);

            // Destroy cards
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);

            // Wait for the frame to end so Unity removes destroyed objects
            yield return new WaitForEndOfFrame();

            // Check remaining active cards
            int activeCards = 0;
            foreach (Transform child in cardGrid)
            {
                if (child.gameObject.activeSelf)
                    activeCards++;
            }

            // If all cards are cleared, move to riddle stage
            if (activeCards <= 0)
            {
                riddleManager?.ShowRiddleUI();
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
        TMP_Text textComponent = newButton.GetComponentInChildren<TMP_Text>();
        textComponent.text = letter;

        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => WordInput_Game.instance.AddLetter(letter));
    }

    public void SetupCards(string word)
    {
        List<char> letters = new List<char>(word.ToUpper());
        letters.AddRange(letters); // create pairs
        letters = letters.OrderBy(x => Random.value).ToList(); // shuffle

        for (int i = 0; i < cardGrid.childCount; i++)
        {
            Card_Game card = cardGrid.GetChild(i).GetComponent<Card_Game>();
            card.letter = letters[i].ToString();
            card.HideCard(); // reset display
        }
    }
}
