using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public string letter;
    public Button button;
    public TMP_Text cardText;

    private bool isRevealed = false;

    void Awake()
    {
        if (cardText == null)
            cardText = GetComponentInChildren<TMP_Text>();

        if (button == null)
            button = GetComponent<Button>();
    }

    public void RevealCard()
    {
        if (isRevealed) return;

        isRevealed = true;

        if (cardText != null)
            cardText.text = letter;

        // ✅ ONLY call the tutorial manager — not GameManager_Game
        if (GameManager.instance != null)
        {
            GameManager.instance.CardRevealed(this);
        }
        else
        {
            Debug.LogWarning("GameManager.instance is null in tutorial scene!");
        }
    }

    public void HideCard()
    {
        isRevealed = false;

        if (cardText != null)
            cardText.text = "?";
    }
}
