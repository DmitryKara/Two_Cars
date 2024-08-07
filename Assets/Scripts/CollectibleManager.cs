using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private int collectedItemCount = 0;
    public TMP_Text collectedItemsText;

    public Countdown countdown;

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
    }

    private void Start()
    {
        UpdateCollectedItemsText();
        countdown.StartCountdown();
    }

    public void AddCollectedItem()
    {
        collectedItemCount++;
        UpdateCollectedItemsText();
    }

    public int GetCollectedItemCount()
    {
        return collectedItemCount;
    }

    public void ResetCollectedItemCount()
    {
        collectedItemCount = 0;
        UpdateCollectedItemsText();
    }

    private void UpdateCollectedItemsText()
    {
        collectedItemsText.text = "Score: " + collectedItemCount;
    }
}
