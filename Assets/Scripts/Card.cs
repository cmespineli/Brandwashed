using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letterText;
    private Button button;
    private char storedLetter;
    private bool isRevealed = false;
    private bool isLocked = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(RevealCard);
        }
        else
        {
            Debug.LogWarning("Button not found on card: " + gameObject.name);
        }

        if (letterText == null)
        {
            letterText = GetComponentInChildren<TextMeshProUGUI>();
            if (letterText == null)
            {
                Debug.LogWarning("TextMeshProUGUI not found on card: " + gameObject.name);
            }
        }

        HideLetter();
    }

    public void SetLetter(char letter)
    {
        storedLetter = letter;
        HideLetter(); // Start with hidden state
    }

    public void RevealCard()
    {
        if (isLocked || isRevealed) return;

        isRevealed = true;
        ShowLetter();
        GameManager.instance.OnCardRevealed(this);
    }

    public void HideLetter()
    {
        isRevealed = false;
        if (letterText != null)
            letterText.text = "?";
    }

    public void ShowLetter()
    {
        if (letterText != null)
            letterText.text = storedLetter.ToString();
    }

    public void Lock()
    {
        isLocked = true;
        SetInteractable(false);
    }

    public void SetInteractable(bool state)
    {
        if (button != null)
            button.interactable = state;
    }

    public bool IsLocked() => isLocked;
    public char GetLetter() => storedLetter;
    public bool IsRevealed() => isRevealed;

    public void ResetCard()
    {
        isRevealed = false;
        isLocked = false;
        HideLetter();
        SetInteractable(true);
    }
}
