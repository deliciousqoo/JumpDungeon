using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dino : Enemy, IMovableEnemy, IShootableEnemy
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private Transform targetPos;
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float range;

    private bool moveCheck = true;
    private float detect_distance;

    public Coroutine playAttackCoroutine;

    private void Start()
    {
        Vector2 tempPos = new Vector2(startPos.x, startPos.y);
        gameObject.GetComponent<Transform>().position = tempPos;
    }

    private void FixedUpdate()
    {
        detect_distance = Vector2.Distance(gameObject.transform.position, targetPos.position);
        
        if (detect_distance > 0.3f && moveCheck)
        {
            SetDirection();
            MovePos();
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
            if (transform.position.x <= startPos.x) { Direction = 1f; sr.flipX = true; }
            else if (transform.position.x >= endPos.x) { Direction = -1f; sr.flipX = false; }
        }
        else
        {
            if (transform.position.x >= startPos.x) { Direction = -1f; sr.flipX = false; }
            else if (transform.position.x <= endPos.x) { Direction = 1f; sr.flipX = true; }
        }
    }

    public IEnumerator ShootAttack()
    {
        moveCheck = false;
        float temp_dir = targetPos.position.x < gameObject.transform.position.x ? -1 : 1;
        bool org_flipX = sr.flipX;
        sr.flipX = temp_dir == 1 ? true : false;
        
        anim.SetTrigger("IsAttack");

        yield return new WaitForSecondsRealtime(0.5f);
        GameObject clone = Instantiate(projectilePrefab);
        clone.GetComponent<DinoProjectile>().SetProjectile(gameObject.transform.position, temp_dir, projectileSpeed, range);

        yield return new WaitForSeconds(0.9f);

        anim.Play("Dino_Idle");
        sr.flipX = org_flipX;
        playAttackCoroutine = null;
        moveCheck = true;
    }
}
