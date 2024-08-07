using UnityEngine;

public class AvoidObstacle : ObstacleMovement
{
    protected override void HandleCollision(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            GameOverManager.Instance.EndGame();
            ReturnToPool();
        }
    }
}
