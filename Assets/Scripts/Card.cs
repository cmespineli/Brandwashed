using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public string letter;
    public Button button;
    public TMP_Text cardText;

    public Sprite[] possibleSprites;
    public Image frontImage;

    private bool isRevealed = false;

    public void RevealCard()
    {
        if (isRevealed) return;

        isRevealed = true;
        cardText.text = letter;
        GameManager_Game.instance.CardRevealed(this);
    }

    public void HideCard()
    {
        isRevealed = false;
        cardText.text = "?";
    }
}
