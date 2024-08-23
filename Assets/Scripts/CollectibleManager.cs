using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private int collectedItemCount = 0;
    public TMP_Text collectedItemsText;

    public Countdown countdown;
    public ObstacleSpawner[] spawners;

    private string scoreString;

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
        scoreString = collectedItemsText.text;
        UpdateCollectedItemsText();
        countdown.StartCountdown();
    }

    public void AddCollectedItem()
    {
        collectedItemCount++;
        UpdateCollectedItemsText();
        UpdateSpawnerChances();
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
        collectedItemsText.text = scoreString + ": " + collectedItemCount;
    }
  
        private void UpdateSpawnerChances()
    {
        foreach (var spawner in spawners)
        {
            spawner.UpdateAvoidObstacleChance(collectedItemCount);
        }
    }

    public void ResetSpawnerChances()
    {
        foreach (var spawner in spawners)
        {
            spawner.ResetObstacleChance();
        }
    }
}