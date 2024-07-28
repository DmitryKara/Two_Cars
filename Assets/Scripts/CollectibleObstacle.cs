using UnityEngine;

public class CollectibleObstacle : ObstacleMovement
{
    protected override void HandleCollision(Collider2D other)
    {
        if (other.CompareTag("Car")) // Убедитесь, что у игрока установлен тег "Player"
        {
            CollectibleManager.Instance.AddCollectedItem();
            ReturnToPool();
        }
    }
}
