using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject sandEffectPrefab;
    [SerializeField]
    private GameObject fireEffectPrefab;

    private Animator anim;
    private BoxCollider2D collider;
    private GameObject tempEffect;
    private SpriteRenderer spriteRenderer;
    private CompositeCollider2D composite;

    private bool getCheck;

    private void Awake()
    {
        composite = GetComponent<CompositeCollider2D>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.name);
        if(!getCheck)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (gameObject.name.Substring(0, 4) == "Sand")
                {
                    StartCoroutine("PlaySandEffect");
                }
                else if (gameObject.name.Substring(0, 4) == "Fire")
                {
                    StartCoroutine("PlayFireEffect");
                }
                else if (gameObject.name.Substring(0, 5) == "Cloud")
                {
                    anim.SetTrigger("IsBounce");
                }
            }
        }
    }

    private IEnumerator PlaySandEffect()
    {
        getCheck = true;
        yield return new WaitForSecondsRealtime(0.5f);
        Color tempColor = spriteRenderer.color;
        tempColor.a = 0f;
        spriteRenderer.color = tempColor;
        composite.isTrigger = true;

        tempEffect = Instantiate(sandEffectPrefab);
        tempEffect.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(tempEffect);

        yield return new WaitForSecondsRealtime(3.0f);

        while(tempColor.a < 1)
        {
            tempColor.a += 0.06f;
            spriteRenderer.color = tempColor;
            yield return new WaitForSecondsRealtime(0.03f);
        }

        composite.isTrigger = false;
        getCheck = false;
    }

    private IEnumerator PlayFireEffect()
    {
        getCheck = true;
        yield return new WaitForSecondsRealtime(0.5f);

        tempEffect = Instantiate(fireEffectPrefab);
        tempEffect.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

        yield return new WaitForSecondsRealtime(0.1f);
        tempEffect.GetComponent<BoxCollider2D>().isTrigger = false;

        yield return new WaitForSecondsRealtime(1f);

        Destroy(tempEffect);
        getCheck = false;
        tempEffect.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    /*private IEnumerator PlayCloudEffect(GameObject player)
    {
        getCheck = true;
        anim.SetTrigger("IsBounce");

        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(0.1f);
        getCheck = false;
    }*/
}
