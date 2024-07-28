using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private int collectedItemCount = 0;
    public TMP_Text collectedItemsText;

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
    }

    public void AddCollectedItem()
    {
        collectedItemCount++;
        UpdateCollectedItemsText();
    }

    private void UpdateCollectedItemsText()
    {
        collectedItemsText.text = "Score: " + collectedItemCount;
    }
}
