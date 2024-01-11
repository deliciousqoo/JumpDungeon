using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonProjectile : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private Vector2 spawnPos;
    public float range;
    public float speed;
    public int dir;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Mathf.Abs(gameObject.transform.position.x - spawnPos.x) > range) Destroy(gameObject);
    }

    public void SetProjectile(Vector2 spawnPos, int dir, float speed, float range)
    {
        if (dir == 1) spriteRenderer.flipX = false;
        else if (dir == -1) spriteRenderer.flipX = true;
        
        this.dir = dir;
        this.speed = speed;
        this.range = range;
        this.spawnPos = spawnPos;

        gameObject.transform.position = spawnPos;
    }

    private void FixedUpdate()
    {
        Vector2 tempPos = new Vector2(transform.position.x + speed * dir, transform.position.y);
        gameObject.transform.position = tempPos;
    }
}
