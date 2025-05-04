using UnityEngine;
using TMPro;

public class HPManager_Game : MonoBehaviour
{
    public static HPManager_Game instance;

    public int maxHP = 3;
    private int currentHP;

    public TMP_Text hpText; // Optional: use images instead

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHP = maxHP;
        UpdateUI();
    }

    public void TakeDamage()
    {
        currentHP--;
        UpdateUI();

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        hpText.text = $"HP: {currentHP}";
    }

    void GameOver()
    {
        Debug.Log("Out of HP! Game Over.");
    }
}
