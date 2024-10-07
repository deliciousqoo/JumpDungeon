using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected BoxCollider2D _collider;
    protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rigid;

    protected virtual void InitializeInfo()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}

public interface IMovable
{
    float Speed { get; set; }
    int Direction { get; set; }
    void Move();
    void ChangeDirection();
}

public interface IAttackable
{
    float DetectionRange { get; set; }
    void Detection();
    void Attack();
}

public interface IShootable
{
    float ProjectileSpeed { get; set; }
    float ShootCycle { get; set; }
    float ShootRange { get; set; }
    void Shoot();
}