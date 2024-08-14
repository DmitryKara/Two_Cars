using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPool obstaclePool;
    public float minSpawnInterval;
    public float maxSpawnInterval;

    public bool isLeftSpawner;

    private float screenWidth;

    [Header("Spawn Chance Settings")]
    public float minAvoidObstacleChance;
    public float maxAvoidObstacleChance;
    public int pointsToMaxChance;

    private float currentAvoidObstacleChance;

    public void Awake()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
        currentAvoidObstacleChance = minAvoidObstacleChance;
    }

    private void Start()
    {
        IntervalNextSpawn();
    }

    void IntervalNextSpawn()
    {
        float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke("SpawnObstacle", randomInterval);
    }

    void SpawnObstacle()
    {
        float[] lanes = GetLanes();
        int laneIndex = Random.Range(0, lanes.Length);
        float spawnX = lanes[laneIndex];

        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 0);

        bool shouldSpawnAvoidObstacle = Random.value <= currentAvoidObstacleChance;

        GameObject obstacle = obstaclePool.GetObject(shouldSpawnAvoidObstacle);
        if (obstacle != null)
        {
            obstacle.transform.position = spawnPosition;
            obstacle.transform.rotation = Quaternion.identity;

            ObstacleMovement obstacleMovement = obstacle.GetComponent<ObstacleMovement>();
            obstacleMovement.pool = obstaclePool;
        }
        else
        {
            Debug.LogError("Failed to get obstacle from pool.");
        }

        IntervalNextSpawn();
    }

    public void ResetObstacleChance()
    {
        currentAvoidObstacleChance = minAvoidObstacleChance;
    }

    public void UpdateAvoidObstacleChance(int currentScore)
    {
        currentAvoidObstacleChance = Mathf.Lerp(
            minAvoidObstacleChance,
            maxAvoidObstacleChance,
            Mathf.Clamp01((float)currentScore / pointsToMaxChance)
        );
    }

    float[] GetLanes()
    {
        float laneWidth = screenWidth / 4f;
        float center = isLeftSpawner ? -screenWidth / 4f : screenWidth / 4f;

        return new float[]
        {
            center - laneWidth / 2f,
            center + laneWidth / 2f
        };
    }
}