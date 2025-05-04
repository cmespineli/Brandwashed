using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letterText;
    private Button button;
    private char storedLetter;
    private bool isRevealed = false;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (letterText == null)
        {
            letterText = GetComponentInChildren<TextMeshProUGUI>();
        }

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
            storedLetter = letter[0];
            HideLetter();
        }
        else
        {
            Debug.LogWarning("Invalid letter passed to AssignLetter: " + letter);
        }
    }

    public void RevealCard()
    {
        if (!isRevealed)
        {
            isRevealed = true;
            letterText.text = storedLetter.ToString();
            GameManager.instance.CardRevealed(this);
        }
    }

    public void ResetCard()
    {
        isRevealed = false;
        letterText.text = "?";
    }

    public void Lock()
    {
        if (button != null)
        {
            button.interactable = false;
        }
    }

    public void Unlock()
    {
        if (button != null)
        {
            button.interactable = true;
        }
        HideLetter();
    }

    private void HideLetter()
    {
        isRevealed = false;
        if (letterText != null)
        {
            letterText.text = "?";
        }
    }

    public char GetLetter()
    {
        return storedLetter;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public bool IsLocked()
    {
        return !GetComponent<Button>().interactable;
    }
}
