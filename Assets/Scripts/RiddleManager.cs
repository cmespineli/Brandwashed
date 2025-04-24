using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class RiddleEntry
{
    public string riddle;
    public string answer; // Must be 5 letters
}

public class RiddleManager : MonoBehaviour
{
    public List<RiddleEntry> riddleBank = new List<RiddleEntry>();
    public TMP_Text riddleText;                       // UI element for showing the riddle
    public WordInput_Game wordInput;                  // Your game scene version of WordInput
    public GameManager_Game gameManager;              // Reference to your gameplay GameManager

    public void LoadNewRiddle()
    {
        if (riddleBank.Count == 0)
        {
            Debug.LogWarning("No riddles in the bank!");
            return;
        }

        int index = Random.Range(0, riddleBank.Count);
        RiddleEntry entry = riddleBank[index];

        riddleText.text = entry.riddle;
        wordInput.SetCorrectWord(entry.answer);
        gameManager.SetupCards(entry.answer);
    }
}
