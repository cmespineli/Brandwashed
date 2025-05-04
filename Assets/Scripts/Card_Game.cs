using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Game : MonoBehaviour
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
        cardText.text = letter;

        GameManager_Game.instance.CardRevealed(this); // game-only manager
    }

    public void HideCard()
    {
        isRevealed = false;
        cardText.text = "?";
    }
}
