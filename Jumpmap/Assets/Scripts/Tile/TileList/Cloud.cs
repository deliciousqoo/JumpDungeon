using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Tile, IApplyForce
{
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void AddForceCall(GameObject player)
    {
        StartCoroutine("AddForce", player);
    }
    public IEnumerator AddForce(GameObject player)
    {
        Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();

        anim.Play("Cloud_Bounce");
        yield return new WaitForSecondsRealtime(0.1f);
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
    }

    /*
    public IEnumerator OnCloudTile()
    {
        yield return new WaitForSecondsRealtime(1 / 7f);

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
        jumpCount = 0;
        anim.SetInteger("jumpCount", 0);

        yield return 0;
    }
    */
}
