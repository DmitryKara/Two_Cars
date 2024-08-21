using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{

    public TMP_Text countdownText;
    public int countdownTime = 3;
    public static bool isCountdownActive { get; private set; } = false;

    public void StartCountdown()
    {
        isCountdownActive = true;

        if (countdownText != null)
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayCountdownMusic();
            StartCoroutine(CountdownRoutine());
        }
    }

    private IEnumerator CountdownRoutine()
    {
        Time.timeScale = 0f;

        countdownText.gameObject.SetActive(true);

        for (int i = countdownTime; i > 0; i--)
        {                   
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.text = "Start!";
        yield return new WaitForSecondsRealtime(1f);

        AudioManager.Instance.PlayRaceMusic();

        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1f;

        isCountdownActive = false;
    }
}