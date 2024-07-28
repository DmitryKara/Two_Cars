using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidObstacle : ObstacleMovement
{
    protected override void HandleCollision(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            Debug.Log("Collision with Car! GAME OVER!!");
            ReturnToPool();
        }
    }
}
