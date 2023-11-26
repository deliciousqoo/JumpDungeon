using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerMovement player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("플레이어 몬스터 충돌");
        Debug.Log(collision.gameObject.name);
        player.OnDamaged(collision.transform.position);
    }*/
}
