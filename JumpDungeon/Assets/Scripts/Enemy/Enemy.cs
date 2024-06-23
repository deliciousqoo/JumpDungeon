using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D collider;

    [SerializeField] private float speed;
    [SerializeField] private float direction;

    public float Speed
    {
        set => speed = Mathf.Max(0, value);
        get => speed;
    }
    public float Direction
    {
        set => direction = value;
        get => direction;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }
}

public interface IMovableEnemy
{
    void MovePos();
    void SetDirection();
}

public interface IShootableEnemy
{
    IEnumerator ShootAttack();
}