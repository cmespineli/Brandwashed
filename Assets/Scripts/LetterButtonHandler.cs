using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LetterButtonHandler : MonoBehaviour
{
    public string assignedLetter;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void SetLetter(string letter)
    {
        assignedLetter = letter;
        GetComponentInChildren<TMP_Text>().text = letter;
    }

    void OnClick()
    {
        WordInput.instance.AddLetter(assignedLetter);
    }
}
