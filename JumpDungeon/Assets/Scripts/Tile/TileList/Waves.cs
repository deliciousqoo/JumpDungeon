using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : Tile, IAffectPlayer, IApplyForce
{
    private PlayerAffectedValue _playerData;
    public PlayerAffectedValue playerData { 
        get { return _playerData; }
        set 
        {
            _playerData = value;
        } 
    }
   
    public IEnumerator AddForce(GameObject player)
    {
        throw new System.NotImplementedException();
    }

    public void AddForceCall(GameObject player)
    {
        Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
        switch (gameObject.name[5])
        {
            case 'R':
                rigid.AddForce(Vector2.right * 30f);
                break;
            case 'L':
                rigid.AddForce(Vector2.left * 30f);
                break;
            case 'U':
                rigid.AddForce(Vector2.up * 20f);
                break;
            case 'D':
                rigid.AddForce(Vector2.down * 20f);
                break;
        }
    }

    public void InitAffectValue()
    {
        PlayerAffectedValue temp = new PlayerAffectedValue();
        temp.maxSpeed = 0.7f;
        temp.jumpPower = 3f;
        temp.speed = 1f;
        temp.drag = 10f;
        temp.gravityScale = 0f;
        temp.jumpCount = 0;

        playerData = temp;
    }
}

