using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Enemy
{
    [SerializeField]
    private Sprite[] anim;

    public float delay;

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
        sr.sprite = anim[2];
        col.size = new Vector2(0f, 0f);
        yield return new WaitForSeconds(0.05f);
        sr.sprite = anim[1];
        col.offset = new Vector2(0, -0.07f);
        col.size = new Vector2(0.06f, 0.04f);
        yield return new WaitForSeconds(0.05f);
        sr.sprite = anim[0];
        col.offset = new Vector2(0, -0.06f);
        col.size = new Vector2(0.06f, 0.06f);
        yield return new WaitForSeconds(0.7f);
        sr.sprite = anim[1];
        col.offset = new Vector2(0, -0.07f);
        col.size = new Vector2(0.06f, 0.04f);
        yield return new WaitForSeconds(0.05f);
        sr.sprite = anim[2];
        col.size = new Vector2(0f, 0f);

        yield return new WaitForSeconds(Speed);

        StartCoroutine("TrapAttack");
    }
}
