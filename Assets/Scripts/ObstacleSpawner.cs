using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPool obstaclePool;
    public float minSpawnInterval = 0.7f;
    public float maxSpawnInterval = 1.5f;

    public bool isLeftSpawner;

    private float screenWidth;

    public void Awake()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
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
        GameObject obstacle = obstaclePool.GetObject();
        obstacle.transform.position = spawnPosition;
        obstacle.transform.rotation = Quaternion.identity;

        ObstacleMovement obstacleMovement = obstacle.GetComponent<ObstacleMovement>();
        obstacleMovement.pool = obstaclePool;

        IntervalNextSpawn();
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
