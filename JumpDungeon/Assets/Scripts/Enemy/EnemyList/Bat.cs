using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bat : Enemy, IMovableEnemy
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;

    private void Start()
    {
        Vector2 tempPos = new Vector2(startPos.x, startPos.y);
        gameObject.GetComponent<Transform>().position = tempPos;
    }

    private void FixedUpdate()
    {
        SetDirection();
        MovePos();
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
}
