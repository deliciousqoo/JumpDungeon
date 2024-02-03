using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField]
    private GameObject coinEffectPrefab;
    [SerializeField]
    private GameObject starEffectPrefab;

    Animator anim;
    BoxCollider2D collider;
    GameObject tempEffect;

    private bool getCheck;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        Debug.DrawRay(collider.bounds.center, gameObject.transform.forward, Color.blue, 0.3f);
        RaycastHit2D rayHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, transform.forward, 0.2f, LayerMask.GetMask("Player"));
        if (rayHit.collider != null)
        {
            if (!getCheck)
            {
                StartCoroutine(PlayEffect(gameObject.name.Substring(0, 4)));
            }
        }
    }

    private IEnumerator PlayEffect(string objectName)   
    {
        switch (gameObject.name.Substring(0, 4))
        {
            case "Coin":
                tempEffect = Instantiate(coinEffectPrefab);
                anim.SetTrigger("IsSpin");
                StartCoroutine("CoinMove");
                break;
            case "Star":
                gameObject.SetActive(false);
                tempEffect = Instantiate(starEffectPrefab);
                break;
        }
        tempEffect.GetComponent<Transform>().position = gameObject.transform.position;
        getCheck = true;

        yield return new WaitForSecondsRealtime(1.0f);

        Destroy(tempEffect);
    }

    private IEnumerator CoinMove()
    {
        Vector2 tempPos = gameObject.transform.position;
        float originPos = gameObject.transform.position.y;
        float targetPos = gameObject.transform.position.y + 0.15f;
        Color tempColor = gameObject.GetComponent<SpriteRenderer>().color;

        while (gameObject.transform.position.y < targetPos)
        {
            tempPos.y += 0.02f;
            gameObject.transform.position = tempPos;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.3f);
        while(gameObject.transform.position.y > originPos)
        {
            tempPos.y -= 0.01f;
            gameObject.transform.position = tempPos;
            tempColor.a -= 0.1f;
            gameObject.GetComponent<SpriteRenderer>().color = tempColor;
            yield return new WaitForSecondsRealtime(0.03f);
        }
    }
}
