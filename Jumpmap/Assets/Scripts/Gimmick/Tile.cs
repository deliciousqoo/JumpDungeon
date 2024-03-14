using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject sandEffectPrefab;

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

    public void EffectStart(string objectName)
    {
        StartCoroutine(PlayEffect(objectName));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(gameObject.name.Substring(0,4) == "Sand")
            {
                StartCoroutine(PlayEffect(gameObject.name.Substring(0, 4)));
            }
            else if(gameObject.name.Substring(0,5) == "Water")
            {
                Debug.Log("check");
            }
        }
    }

    private IEnumerator PlayEffect(string objectName)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Color tempColor = spriteRenderer.color;
        tempColor.a = 0f;
        spriteRenderer.color = tempColor;
        //collider.isTrigger = true;
        composite.isTrigger = true;

        switch (objectName)
        {
            case "Sand":
                tempEffect = Instantiate(sandEffectPrefab);
                break;
        }
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

        //collider.isTrigger = false;
        composite.isTrigger = false;
    }
}
