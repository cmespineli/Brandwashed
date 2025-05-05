using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Game : MonoBehaviour
{
    public string letter;
    public Button button;
    public TMP_Text cardText;

    [SerializeField] private Image cardImage;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;

    private bool isRevealed = false;

    void Awake()
    {
        if (cardText == null)
            cardText = GetComponentInChildren<TMP_Text>();
        if (button == null)
            button = GetComponent<Button>();
        if (cardImage == null)
            cardImage = GetComponent<Image>();

        button.onClick.AddListener(RevealCard);
    }

    public void AssignLetter(string assignedLetter)
    {
        letter = assignedLetter;
        HideCard();
    }

    public void RevealCard()
    {
        if (isRevealed || string.IsNullOrEmpty(letter)) return;

        isRevealed = true;
        cardText.text = letter;

        if (cardImage != null && frontSprite != null)
            cardImage.sprite = frontSprite;

        GameManager_Game.instance.CardRevealed(this);
    }

    public void HideCard()
    {
        isRevealed = false;
        cardText.text = " ";

        if (cardImage != null && backSprite != null)
            cardImage.sprite = backSprite;
    }
}
