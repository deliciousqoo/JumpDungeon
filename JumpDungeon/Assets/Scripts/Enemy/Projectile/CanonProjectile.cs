using UnityEngine;

public class CanonProjectile : MonoBehaviour, IMovable
{
    private SpriteRenderer _spriteRenderer;

    public float Speed { get; set; }
    public int Direction { get; set; }
    public float Range { get; set; }

    public void Setup(float speed, int direction, float range)
    {
        Speed = speed;
        Direction = direction;
        Range = range;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.flipX = direction == 1 ? false : true;
    }

    private void FixedUpdate()
    {
        Move();
        CheckRange();
    }

    public void Move()
    {
        Vector2 currentPosition = transform.localPosition;
        currentPosition.x += Speed * Direction * Time.fixedDeltaTime;
        transform.localPosition = currentPosition;
    }

    public void ChangeDirection()
    {
    }

    private void CheckRange()
    {
        if (Mathf.Abs(transform.localPosition.x) >= Range)
        {
            Destroy(gameObject);
        }
    }
}
