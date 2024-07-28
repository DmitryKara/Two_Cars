using UnityEngine;

public class CollectibleObstacle : ObstacleMovement
{
    protected override void HandleCollision(Collider2D other)
    {
        if (other.CompareTag("Car")) // ���������, ��� � ������ ���������� ��� "Player"
        {
            CollectibleManager.Instance.AddCollectedItem();
            ReturnToPool();
        }
    }
}
