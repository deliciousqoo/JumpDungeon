using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonProjectile : Projectile
{
    private void FixedUpdate()
    {
        Vector2 tempPos = new Vector2(transform.position.x + Speed * Dir, transform.position.y);
        gameObject.transform.position = tempPos;
        if (Mathf.Abs(gameObject.transform.position.x - SpawnPos.x) > Range) Destroy(gameObject);
    }
    public override void SetProjectile(Vector2 spawnPos, float dir, float speed, float range)
    {
        sr.flipX = dir == -1 ? true : false;

        this.Dir = dir;
        this.Speed = speed;
        this.Range = range;
        this.SpawnPos = spawnPos;

        gameObject.transform.position = spawnPos;
    }
}
