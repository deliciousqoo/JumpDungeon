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

    private bool getCheck;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    

    public void EffectStart(string objectName)
    {
        StartCoroutine(PlayEffect(objectName));
    }

    private IEnumerator PlayEffect(string objectName)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Color tempColor = spriteRenderer.color;
        tempColor.a = 0f;
        spriteRenderer.color = tempColor;
        collider.isTrigger = true;

        switch (objectName)
        {
            case "Sand":
                tempEffect = Instantiate(sandEffectPrefab);
                break;
        }
        tempEffect.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x + 0.04f, gameObject.transform.position.y - 0.04f, 0);
        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(tempEffect);

        yield return new WaitForSecondsRealtime(3.0f);

        while(tempColor.a < 1)
        {
            tempColor.a += 0.06f;
            spriteRenderer.color = tempColor;
            yield return new WaitForSecondsRealtime(0.03f);
        }

        collider.isTrigger = false;
    }
}
