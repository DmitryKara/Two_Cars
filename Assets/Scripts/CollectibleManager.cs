using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private int collectedItemCount = 0;
    public TMP_Text collectedItemsText;

    public Countdown countdown;
    public ObstacleSpawner[] spawners;

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
        collectedItemsText.text = "Score: " + collectedItemCount;
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