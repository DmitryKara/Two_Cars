using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public GameObject gameOverPanel;
    public TMP_Text resultText;
    public Countdown countdown;

    public HighScoreManager highScoreManager;
    private int score;

    public Pause pauseScript;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameOverPanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;

        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.ResetCollectedItemCount();
        }

        ObstacleMovement[] obstacle = FindObjectsOfType<ObstacleMovement>();
        foreach (ObstacleMovement obs in obstacle)
        {
            obs.ResetToObstacle();
        }

        CarController[] cars = FindObjectsOfType<CarController>();
        foreach (CarController car in cars)
        {
            car.ResetCar();
        }

        if (countdown != null)
        {
            countdown.gameObject.SetActive(true);
            countdown.countdownText.gameObject.SetActive(true);
            countdown.StartCountdown();
        }

        pauseScript.RestartDuringPause();
        pauseScript.EnablePauseButton();
    }
    public void ResultText()
    {
        resultText.text = resultText.text + ": " + score;
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        score = CollectibleManager.Instance.GetCollectedItemCount();
        ResultText();
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMenuMusic();

        if (pauseScript != null)
        {
            pauseScript.DisablePauseButton();
        }

        CollectibleManager.Instance.ResetSpawnerChances();

        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.AddScore(score);
        }
        else
        {
            Debug.LogError("HighScoreManager не найден!");
        }
    }
}
