using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hedgehog : Enemy, IMovableEnemy
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private Transform targetPos;

    private float detect_distance;
    private bool moveCheck = true;

    private Coroutine playAttackCoroutine;

    private void Start()
    {
        col.isTrigger = true;
        Vector2 tempPos = new Vector2(startPos.x, startPos.y);
        gameObject.GetComponent<Transform>().position = tempPos;
    }

    private void FixedUpdate()
    {
        detect_distance = Vector2.Distance(gameObject.transform.position, targetPos.position);
        SetDirection();
        if(detect_distance > 0.15f && moveCheck)
        {
            MovePos();
            col.offset = new Vector2(0.015f * Direction, -0.115f);
        }
        else
        {
            if (playAttackCoroutine == null) { playAttackCoroutine = StartCoroutine(PlayAttack()); }
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

    private IEnumerator PlayAttack()
    {
        moveCheck = false;
        col.isTrigger = false;
        gameObject.GetComponent<Animator>().SetTrigger("IsAttack");

        yield return new WaitForSecondsRealtime(0.3f);
        col.offset = new Vector2(0.01f * Direction, -0.08f);
        col.size = new Vector2(0.25f, 0.085f);

        yield return new WaitForSecondsRealtime(0.5f);

        col.isTrigger = true;
        col.offset = new Vector2(0.015f * Direction, -0.115f);
        col.size = new Vector2(0.07f, 0.01f);

        yield return new WaitForSecondsRealtime(1.0f);

        moveCheck = true;
        playAttackCoroutine = null;
    }
}
