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
    public static RiddleManager instance;

    public List<RiddleEntry> riddleBank = new List<RiddleEntry>();
    public TMP_Text riddleText;
    public WordInput_Game wordInput;
    public GameManager_Game gameManager;

    void Awake()
    {
        instance = this;
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
        // Optional: disable card grid
        if (gameManager.cardGrid != null)
            gameManager.cardGrid.gameObject.SetActive(false);

        // Show riddle UI and letter input panel
        if (riddleText != null)
            riddleText.gameObject.SetActive(true);
        if (wordInput != null)
            wordInput.gameObject.SetActive(true);
        if (gameManager.buttonPanel != null)
            gameManager.buttonPanel.gameObject.SetActive(true);
    }

    private void InitializeRiddles()
    {
        riddleBank = new List<RiddleEntry>
        {
            new RiddleEntry { riddle = "I fall from the sky, block the sun, and sting the eyes. What am I?", answer = "ASHES" },
            new RiddleEntry { riddle = "Born in war, I glow at night, deadly in silence, a toxic fright.", answer = "GLOOM" },
            new RiddleEntry { riddle = "I'm hoarded, scarce, and make men kill. I'm liquid life, yet never still.", answer = "WATER" },
            new RiddleEntry { riddle = "I shelter few in times of need. My vaults hold fear, not noble deed.", answer = "BUNKR" },
            new RiddleEntry { riddle = "You breathe me in, you cough me out. I’m not your friend, without a doubt.", answer = "SMOGS" },
            new RiddleEntry { riddle = "No longer green, I used to grow. Now I’m cracked and dry below.", answer = "EARTH" },
            new RiddleEntry { riddle = "I ride the wind, I spread the flame. In silent nights, I end the game.", answer = "FIRES" },
            new RiddleEntry { riddle = "Once a helper, now I hunt. I have no soul, just metal grunt.", answer = "DRONE" },
            new RiddleEntry { riddle = "I used to beep, now I scream. I light the dark in every dream.", answer = "SIREN" },
            new RiddleEntry { riddle = "I feed on waste, I skitter and squeak. I outlast you though I am weak.", answer = "RATTS" }
        };
    }
}
