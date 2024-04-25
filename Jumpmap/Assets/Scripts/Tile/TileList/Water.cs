using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Tile, IAffectPlayer
{
    private PlayerAffectedValue _playerData;
    public PlayerAffectedValue playerData 
    {
        get { return _playerData; }
        set
        {
            _playerData = value;
        }
    }

    public void InitAffectValue()
    {
        PlayerAffectedValue temp = new PlayerAffectedValue();
        temp.maxSpeed = 0.7f;
        temp.jumpPower = 3f;
        temp.speed = 1f;
        temp.drag = 10f;
        temp.gravityScale = 0.3f;
        temp.jumpCount = 0;

        playerData = temp;
    }
}