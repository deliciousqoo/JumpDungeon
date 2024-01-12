using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;

    [SerializeField]
    private Sprite[] anim;

    public float attackSpeed;
    public float delay;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine("SetDelay");
    }

    private IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine("TrapAttack");
    }

    private IEnumerator TrapAttack()
    {
        spriteRenderer.sprite = anim[2];
        collider.size = new Vector2(0f, 0f);
        //collider.isTrigger = true;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.sprite = anim[1];
        collider.offset = new Vector2(0, -0.07f);
        collider.size = new Vector2(0.06f, 0.04f);
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.sprite = anim[0];
        collider.offset = new Vector2(0, -0.06f);
        collider.size = new Vector2(0.06f, 0.06f);
        yield return new WaitForSeconds(0.7f);
        spriteRenderer.sprite = anim[1];
        collider.offset = new Vector2(0, -0.07f);
        collider.size = new Vector2(0.06f, 0.04f);
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.sprite = anim[2];
        collider.size = new Vector2(0f, 0f);
        //collider.isTrigger = true;

        yield return new WaitForSeconds(attackSpeed);

        StartCoroutine("TrapAttack");
    }
}
