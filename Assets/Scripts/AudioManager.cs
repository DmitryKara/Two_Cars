using System.Collections;
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
    public AudioClip collection;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private bool isSoundOn;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            musicSource.loop = true;

            LoadAudioSettings();
            ApplyAudioSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadAudioSettings()
    {
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
    }

    public void ApplyAudioSettings()
    {
        AudioListener.volume = isSoundOn ? 1 : 0;
        if (musicSource.isPlaying)
        {
            musicSource.volume = isSoundOn ? 1 : 0;
        }
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        ApplyAudioSettings();
        SaveAudioSettings();
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

    public void PlayCollections()
    {
        PlaySFX(collection);
    }
}