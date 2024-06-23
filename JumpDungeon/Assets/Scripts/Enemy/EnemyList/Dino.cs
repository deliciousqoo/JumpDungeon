using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : Enemy, IMovableEnemy, IShootableEnemy
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private Transform targetPos;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float range;

    [SerializeField] private Sprite[] anim;
    [SerializeField] private GameObject projectilePrefab;

    private bool moveCheck = true;

    private Coroutine playAttackCoroutine;

    private void Start()
    {
        Vector2 tempPos = new Vector2(startPos.x, startPos.y);
        gameObject.GetComponent<Transform>().position = tempPos;
    }

    private void FixedUpdate()
    {
        range = Vector2.Distance(gameObject.transform.position, targetPos.position);
        SetDirection();
        
        if (range > 0.3f && moveCheck)
        {
            MovePos();
            collider.offset = new Vector2(0.005f * Direction, -0.02f);
        }
        else
        {
            if (playAttackCoroutine == null) { playAttackCoroutine = StartCoroutine(ShootAttack()); }
        }
    }

    public void MovePos()
    {
        Vector2 tempPos = new Vector2(transform.position.x + Speed * Direction, transform.position.y);
        gameObject.transform.position = tempPos;
    }

    public void SetDirection()
    {
        if (startPos.x < endPos.x)
        {
            if (transform.position.x <= startPos.x) { Direction = 1f; spriteRenderer.flipX = true; }
            else if (transform.position.x >= endPos.x) { Direction = -1f; spriteRenderer.flipX = false; }
        }
        else
        {
            if (transform.position.x >= startPos.x) { Direction = -1f; spriteRenderer.flipX = false; }
            else if (transform.position.x <= endPos.x) { Direction = 1f; spriteRenderer.flipX = true; }
        }
    }

    public IEnumerator ShootAttack()
    {
        GameObject clone = Instantiate(projectilePrefab);
        clone.GetComponent<CanonProjectile>().SetProjectile(gameObject.transform.position, Direction, projectileSpeed, range);

        yield return 0;
    }
}
