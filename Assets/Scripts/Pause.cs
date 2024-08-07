using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject gameOverPanel;
    public Button pauseButton;
    public Countdown countdown;
    public TMP_Text resultText;

    private void Start()
    {
        isPaused = false;
    }

    public void TogglePause()
    {
        if (countdown != null && countdown.isCountdownActive)
        {
            return;
        }

        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        resultText.text = "YOUR RESULT: " + CollectibleManager.Instance.GetCollectedItemCount();
        gameOverPanel.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        gameOverPanel.SetActive(false);
    }

    public void DisablePauseButton()
    {
        pauseButton.gameObject.SetActive(false);
    }

    public void EnablePauseButton()
    {
        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(true);
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            if (!isPaused)
            {
                PauseGame();
            }
        }
    }
}