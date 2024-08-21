using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip menuMusic;
    public AudioClip countdownMusic;
    public AudioClip raceMusic;
    public AudioClip collisionSound;
    public AudioClip buttonClickSound;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            musicSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnOffSound()
    {
        if (MuteButton.Instance != null)
        {
            MuteButton.Instance.OnOffSound();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource != null && clip != null)
        {
            if (musicSource.clip == clip)
                return;

            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("musicSource or clip is null!");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("sfxSource or clip is null!");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayCountdownMusic()
    {
        PlayMusic(countdownMusic);
    }

    public void PlayRaceMusic()
    {
        PlayMusic(raceMusic);
    }

    public void PlayCollisionSound()
    {
        PlaySFX(collisionSound);
    }

    public void PlayButtonClickSound()
    {
        PlaySFX(buttonClickSound);
    }
}
