using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy, IMovable
{
    public Vector2 StartPos;
    public Vector2 EndPos;
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public int Direction { get; set; }

    private void Start()
    {
        transform.position = StartPos;
        InitializeInfo();
    }

    protected override void InitializeInfo()
    {
        base.InitializeInfo();

        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Move();
        ChangeDirection();
    }

    public void Move()
    {
        Vector2 currentPosition = transform.position;
        currentPosition.x += Speed * Direction * Time.fixedDeltaTime;
        transform.position = currentPosition;
    }

    public void ChangeDirection()
    {
        if (Direction == 1 && transform.position.x >= EndPos.x)
        {
            _spriteRenderer.flipX = false;
            Direction = -1;
        }
        else if (Direction == -1 && transform.position.x <= StartPos.x)
        {
            _spriteRenderer.flipX = true;
            Direction = 1;
        }
    }
}
