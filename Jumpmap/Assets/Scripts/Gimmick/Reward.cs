using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField]
    private GameObject coinEffectPrefab;
    [SerializeField]
    private GameObject starEffectPrefab;
    [SerializeField]
    private GameObject clearGateEffectPrefab;
    [SerializeField]
    private GameObject[] candles;

    private Animator anim;
    private BoxCollider2D collider;
    private GameObject tempEffect;
    private SpriteRenderer spriteRenderer;

    private Color tempColor;
    private bool getCheck;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Debug.Log(objectName);
        if(objectName == "Gate") {
            Debug.Log("check");
            getCheck = true;
            StageManager.instance.StageProgressSetUp();
            GameManager.instance.OnCompletedCall(gameObject.transform.position);
            yield return new WaitUntil(() => Mathf.Abs(gameObject.transform.position.x - GameManager.instance.playerPos.x) < 0.01f);
            yield return new WaitForSecondsRealtime(0.5f);

            tempEffect = Instantiate(clearGateEffectPrefab);
            GameManager.instance.PlayerHide();
            tempEffect.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x + 0.02f, gameObject.transform.position.y - 0.015f, 0f);

            //yield return new WaitForSecondsRealtime(0.8f);
            for(int i=0;i<candles.Length;i++)
            {
                candles[i].GetComponent<Animator>().SetTrigger("IsClear");
            }

            yield return new WaitForSecondsRealtime(2.0f);

            GameManager.instance.OnCompletedPanelCall();
        }
        else
        {
            switch (objectName)
            {
                case "Coin":
                    tempEffect = Instantiate(coinEffectPrefab);
                    anim.SetTrigger("IsSpin");
                    StartCoroutine("CoinMove");
                    break;
                case "Star":
                    //StageManager.instance.StageProgressSetUp();
                    StageManager.instance.stageProgressStep++;
                    tempColor.a = 0f;
                    spriteRenderer.color = tempColor;
                    tempEffect = Instantiate(starEffectPrefab);
                    break;
            }
            tempEffect.GetComponent<Transform>().position = gameObject.transform.position;
            getCheck = true;
            
        }
        yield return new WaitForSecondsRealtime(0.5f);

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
            tempColor.a -= 0.06f;
            gameObject.GetComponent<SpriteRenderer>().color = tempColor;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        gameObject.SetActive(false);
    }
}
