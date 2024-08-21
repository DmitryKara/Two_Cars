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
    public Sprite pause;
    public Sprite play;

    private Image pauseButtonImage;

    public bool IsPaused
    {
        get
        {
            return isPaused;
        }
        set 
        {
            isPaused = value;
        }
    }

    private void Start()
    {
        if (pauseButton != null)
        {
            pauseButtonImage = pauseButton.GetComponent<Image>();
        }

        isPaused = false;
    }

    public void TogglePause()
    {
        if (countdown != null && Countdown.isCountdownActive)
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

        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMenuMusic();

        if (pauseButtonImage != null)
        {
            pauseButtonImage.sprite = play;
        }
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);

        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayRaceMusic();

        RestartDuringPause();
    }

    public void RestartDuringPause()
    {
        isPaused = false;

        if(pauseButtonImage != null)
        {
            pauseButtonImage.sprite = pause;
        }
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