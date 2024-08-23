using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    private void Start()
    {
        UpdateHighScoreDisplay();
    }

    public void UpdateHighScoreDisplay()
    {
        if (HighScoreManager.Instance == null)
        {
            Debug.LogError("HighScoreManager не найден!");
            return;
        }

        highScoreText.text = "";

        foreach (HighScore score in HighScoreManager.Instance.highScores)
        {
            highScoreText.text += $"{score.Date} {' ',3} {score.Time} {' ',3} {score.Score}\n\n";
        }
    }
}