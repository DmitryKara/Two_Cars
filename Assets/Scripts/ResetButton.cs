using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public Button resetButton;
    public HighScoreDisplay highScoreDisplay;

    void Start()
    {
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(OnResetButtonClick);
        }
        else
        {
            Debug.LogError("������ ������ �� �����������!");
        }
    }

    void OnResetButtonClick()
    {
        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.ResetHighScores();

            if (highScoreDisplay != null)
            {
                highScoreDisplay.UpdateHighScoreDisplay();
            }
            else
            {
                Debug.LogError("HighScoreDisplay �� ������!");
            }
        }
        else
        {
            Debug.LogError("HighScoreManager �� ������!");
        }
    }
}
