using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text pairsText;
    public TMP_Text riddlesText;
    public TMP_Text roundsText;
    public TMP_Text timeText;

    void Start()
    {
        scoreText.text = $"Final Score: {GameStats.finalScore}";
        pairsText.text = $"Card Pairs Matched: {GameStats.cardPairsMatched}";
        riddlesText.text = $"Riddles Solved: {GameStats.riddlesSolved}";
        roundsText.text = $"Rounds Passed: {GameStats.roundsPassed}";
        timeText.text = $"Time Survived: {Mathf.FloorToInt(GameStats.totalTimeSurvived)} seconds";
    }
}
