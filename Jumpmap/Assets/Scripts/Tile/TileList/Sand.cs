using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : Tile, IPlayEffect
{
    [SerializeField]
    private GameObject EffectPrefab;

    private GameObject tempEffect;
    private bool getCheck;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        composite = GetComponent<CompositeCollider2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(!getCheck && collision.gameObject.tag == "Player")
        {
            StartCoroutine("PlayerEffect");
        }
    }

    public IEnumerator PlayerEffect()
    {
        getCheck = true;
        yield return new WaitForSecondsRealtime(0.5f);
        Color tempColor = spriteRenderer.color;
        tempColor.a = 0f;
        spriteRenderer.color = tempColor;
        composite.isTrigger = true;

        tempEffect = Instantiate(EffectPrefab);
        tempEffect.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(tempEffect);

        yield return new WaitForSecondsRealtime(3.0f);

        while (tempColor.a < 1)
        {
            tempColor.a += 0.06f;
            spriteRenderer.color = tempColor;
            yield return new WaitForSecondsRealtime(0.03f);
        }

        composite.isTrigger = false;
        getCheck = false;
    }
}
