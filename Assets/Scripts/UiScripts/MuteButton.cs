using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Sprite soundOn;
    public Sprite soundOff;

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        UpdateButtonSprite();
    }

    public void OnOffSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleSound();
            UpdateButtonSprite();
        }
    }

    private void UpdateButtonSprite()
    {
        bool isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        buttonImage.sprite = isSoundOn ? soundOn : soundOff;
    }
}