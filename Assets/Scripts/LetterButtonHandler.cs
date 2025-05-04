using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterButtonHandler : MonoBehaviour
{
    private string letter;

    public void SetLetter(string newLetter)
    {
        letter = newLetter;

        TMP_Text text = GetComponentInChildren<TMP_Text>();
        if (text != null)
        {
            text.text = letter;
        }

        GetComponent<Button>().onClick.RemoveAllListeners();

        GetComponent<Button>().onClick.AddListener(() =>
        {
            // Try tutorial version first
            if (WordInputExists())
            {
                WordInput.instance.AddLetter(letter);
            }
            else if (WordInput_GameExists())
            {
                WordInput_Game.instance.AddLetter(letter);
            }
            else
            {
                Debug.LogWarning("⚠ No WordInput or WordInput_Game instance found.");
            }
        });
    }

    private bool WordInputExists()
    {
#if UNITY_EDITOR
        return WordInput.instance != null;
#else
        return FindObjectOfType<WordInput>() != null;
#endif
    }

    private bool WordInput_GameExists()
    {
#if UNITY_EDITOR
        return WordInput_Game.instance != null;
#else
        return FindObjectOfType<WordInput_Game>() != null;
#endif
    }
}
