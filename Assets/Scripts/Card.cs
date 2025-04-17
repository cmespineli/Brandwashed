using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public string letter;
    public bool isRevealed = false;
    public Button button;

    public void RevealCard()
    {
        if (isRevealed) return;
        isRevealed = true;
        button.GetComponentInChildren<TMP_Text>().text = letter;
        GameManager.instance.CardRevealed(this);
    }

    public void HideCard()
    {
        isRevealed = false;
        button.GetComponentInChildren<TMP_Text>().text = "?";
    }
}
