// RiddleManager.cs
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
    public TMP_Text riddleText;
    public WordInput_Game wordInput;
    public GameManager_Game gameManager;

    void Awake()
    {
        InitializeRiddles();
    }

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

    public void ShowRiddleUI()
    {
        gameManager.cardGrid.gameObject.SetActive(false);
        riddleText.gameObject.SetActive(true);
        wordInput.gameObject.SetActive(true);
        gameManager.buttonPanel.gameObject.SetActive(true);
        wordInput.inputControlsPanel.SetActive(true); // <== THIS IS CRUCIAL
    }


    void InitializeRiddles()
    {
        riddleBank = new List<RiddleEntry>
        {
            new RiddleEntry { riddle = "Scattered and torn, I'm what's left behind when cities burn.", answer = "ASHEN" },
            new RiddleEntry { riddle = "I light up nights in cities lost, remnants of a powered cost.", answer = "LIGHT" },
            new RiddleEntry { riddle = "I crawl through ruins, seeking sound, where no human should be found.", answer = "SNIPE" },
            new RiddleEntry { riddle = "I'm counted in breaths behind a mask, to survive is my only task.", answer = "VITAL" },
            new RiddleEntry { riddle = "No green remains, I fall like dust, I once was shelter, now just rust.", answer = "METAL" },
            new RiddleEntry { riddle = "Hollow eyes behind a screen, I move but never dream.", answer = "ALONE" },
            new RiddleEntry { riddle = "The signal fades, my voice is gone, but in the dark, I carry on.", answer = "SOUND" },
            new RiddleEntry { riddle = "Echoes of life in concrete halls, I haunt these man-made walls.", answer = "GHOST" },
            new RiddleEntry { riddle = "Shattered glass beneath my feet, no more signs, no more street.", answer = "GLASS" },
            new RiddleEntry { riddle = "I shine without warmth in a sky of ash, memories of a brighter flash.", answer = "SOLAR" }
        };
    }
}
