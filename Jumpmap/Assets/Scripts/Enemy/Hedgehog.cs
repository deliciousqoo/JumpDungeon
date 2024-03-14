using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
    [SerializeField]
    private Vector2 startPos;
    [SerializeField]
    private Vector2 endPos;
    [SerializeField]
    private Transform targetPos;

    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;

    private float speed = 0.0025f;
    private float direction;
    private float detect_range = 100f;

    

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        collider.isTrigger = true;
        Vector2 tempPos = new Vector2(startPos.x, startPos.y);
        gameObject.GetComponent<Transform>().position = tempPos;
        StartCoroutine("PlayIdle");
    }

    private void Update()
    {
        detect_range = Vector2.Distance(gameObject.transform.position, targetPos.position);

        if (startPos.x < endPos.x)
        {
            if (transform.position.x <= startPos.x) { direction = 1f; spriteRenderer.flipX = true; }
            else if (transform.position.x >= endPos.x) { direction = -1f; spriteRenderer.flipX = false; }
        }
        else
        {
            if (transform.position.x >= startPos.x) { direction = -1f; spriteRenderer.flipX = false; }
            else if (transform.position.x <= endPos.x) { direction = 1f; spriteRenderer.flipX = true; }
        }
    }

    private IEnumerator PlayIdle()
    {
        while (detect_range > 0.15f)
        {
            Vector2 tempPos = new Vector2(transform.position.x + speed * direction, transform.position.y);
            gameObject.transform.position = tempPos;
            collider.offset = new Vector2(0.015f * direction, -0.115f);
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine("PlayAttack");
    }

    private IEnumerator PlayAttack()
    {
        collider.isTrigger = false;
        StopCoroutine("PlayIdle");
        gameObject.GetComponent<Animator>().SetTrigger("IsAttack");

        yield return new WaitForSecondsRealtime(0.3f);
        collider.offset = new Vector2(0.01f*direction, -0.08f);
        collider.size = new Vector2(0.18f, 0.085f);

        yield return new WaitForSecondsRealtime(0.5f);

        collider.isTrigger = true;
        collider.offset = new Vector2(0.015f * direction, -0.115f);
        collider.size = new Vector2(0.07f, 0.01f);
        
        yield return new WaitForSecondsRealtime(1.0f);

        StartCoroutine("PlayIdle");
    }
}
