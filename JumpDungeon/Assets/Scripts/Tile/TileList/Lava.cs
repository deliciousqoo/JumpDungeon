using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : Tile, IPlayEffect
{
    [SerializeField]
    private GameObject EffectPrefab;

    private GameObject tempEffect;
    private bool getCheck;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!getCheck && collision.gameObject.tag == "Player")
        {
            StartCoroutine("PlayerEffect");
        }
    }
    public IEnumerator PlayerEffect()
    {
        getCheck = true;
        yield return new WaitForSecondsRealtime(0.5f);

        tempEffect = Instantiate(EffectPrefab);
        tempEffect.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

        yield return new WaitForSecondsRealtime(0.1f);
        tempEffect.GetComponent<BoxCollider2D>().isTrigger = false;

        yield return new WaitForSecondsRealtime(1f);

        Destroy(tempEffect);
        getCheck = false;
        tempEffect.GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
