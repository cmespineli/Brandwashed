using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letterText;
    [SerializeField] private Image cardImage;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;

    private Button button;
    private string storedLetter;
    private bool isRevealed = false;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (letterText == null)
            letterText = GetComponentInChildren<TextMeshProUGUI>();
        if (cardImage == null)
            cardImage = GetComponent<Image>();

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(RevealCard);
        }

        HideLetter();
    }

    public void AssignLetter(string letter)
    {
        if (!string.IsNullOrEmpty(letter) && letter.Length == 1)
        {
            storedLetter = letter;
            HideLetter();
        }
        else
        {
            Debug.LogWarning("Invalid letter assigned: " + letter);
        }
    }

    public void RevealCard()
    {
        if (isRevealed) return;

        isRevealed = true;
        letterText.text = storedLetter;

        if (cardImage != null && frontSprite != null)
            cardImage.sprite = frontSprite;

        GameManager.instance.CardRevealed(this);
    }

    public void HideLetter()
    {
        isRevealed = false;
        letterText.text = " ";

        if (cardImage != null && backSprite != null)
            cardImage.sprite = backSprite;
    }

    public void SetInteractable(bool interactable)
    {
        if (button != null)
            button.interactable = interactable;
    }

    public string GetLetter()
    {
        return storedLetter;
    }
}
