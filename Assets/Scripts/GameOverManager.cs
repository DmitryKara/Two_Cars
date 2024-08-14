using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public GameObject gameOverPanel;
    public TMP_Text resultText;
    public Countdown countdown;

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

        pauseScript.EnablePauseButton();
    }
    public void EndGame()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        resultText.text = "YOUR RESULT: " + CollectibleManager.Instance.GetCollectedItemCount();

        if (pauseScript != null)
        {
            pauseScript.DisablePauseButton();
        }

        CollectibleManager.Instance.ResetSpawnerChances();
    }
}
