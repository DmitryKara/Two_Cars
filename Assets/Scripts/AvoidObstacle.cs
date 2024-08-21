using UnityEngine;

public class AvoidObstacle : ObstacleMovement
{
    protected override void HandleCollision(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayCollisionSound();

            GameOverManager.Instance.EndGame();
            ReturnToPool();
        }
    }
}
