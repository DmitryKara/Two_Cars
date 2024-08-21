using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public static MuteButton Instance { get; private set; }

    public Sprite soundOn;
    public Sprite soundOff;

    private Image buttonImage;
    private bool isSoundOn;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        UpdateButtonSprite();
        AudioListener.volume = isSoundOn ? 1 : 0;
    }

    public void OnOffSound()
    {
        isSoundOn = !isSoundOn;
        AudioListener.volume = isSoundOn ? 1 : 0;
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        buttonImage.sprite = isSoundOn ? soundOn : soundOff;
    }
}
