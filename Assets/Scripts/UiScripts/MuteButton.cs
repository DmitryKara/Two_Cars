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

        buttonImage.sprite = soundOn;
    }

    public void OnOffSound()
    {
        if (buttonImage.sprite == soundOn)
        {
            buttonImage.sprite = soundOff;
        }
        else
        {
            buttonImage.sprite = soundOn;
        }
    }
}
