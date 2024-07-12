using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public SpriteRenderer sr;
    public BoxCollider2D col;
    protected Animator anim;

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
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
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