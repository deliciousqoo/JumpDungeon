using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bat : MonoBehaviour
{
    [SerializeField]
    private Vector2 startPos;
    [SerializeField]
    private Vector2 endPos;

    SpriteRenderer spriteRenderer;

    private float speed = 0.005f;
    private float direction;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Vector2 tempPos = new Vector2(startPos.x, startPos.y - 9.5f);
        gameObject.GetComponent<Transform>().position = tempPos;
    }

    private void Update()
    {
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

    private void FixedUpdate()
    {
        Vector2 tempPos = new Vector2(transform.position.x + speed * direction, transform.position.y);
        gameObject.transform.position = tempPos;
    }
}
