using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterButtonHandler : MonoBehaviour
{
    private string letter;

    public void SetLetter(string newLetter)
    {
        letter = newLetter;

        // Update visible letter text
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        if (text != null)
        {
            text.text = letter;
        }

        // Prevent double-adding listeners
        GetComponent<Button>().onClick.RemoveAllListeners();

        // Add listener to pass letter to WordInput_Game
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (WordInput_Game.instance != null)
            {
                WordInput_Game.instance.AddLetter(letter);
            }
            else
            {
                Debug.LogWarning("WordInput_Game.instance is null");
            }
        });
    }
}
