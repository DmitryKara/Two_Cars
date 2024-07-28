using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed;
    public ObjectPool pool;

    void Update()
    {
        Move();
        CheckOutOfBounds();
    }

    protected void Move()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    protected void CheckOutOfBounds()
    {
        if (transform.position.y < -10f)
        {
            ReturnToPool();
        }
    }

    protected void ReturnToPool()
    {
        if (pool != null)
        {
            pool.ReturnObject(gameObject);
        }
    }

    protected virtual void HandleCollision(Collider2D other)
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other);
    }
}
