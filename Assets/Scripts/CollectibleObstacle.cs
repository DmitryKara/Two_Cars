using UnityEngine;

public class CollectibleObstacle : ObstacleMovement
{
    protected override void HandleCollision(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            AudioManager.Instance.PlayCollections();
            CollectibleManager.Instance.AddCollectedItem();
            ReturnToPool();
        }

        if (other.CompareTag("Finish"))
        {
            GameOverManager.Instance.EndGame();
        }
    }
}