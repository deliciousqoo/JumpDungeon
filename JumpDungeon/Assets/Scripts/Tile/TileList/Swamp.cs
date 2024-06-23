using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : Tile, IAffectPlayer
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
        temp.jumpPower = 1.5f;
        temp.speed = 0.1f;
        temp.drag = 50f;
        temp.gravityScale = 0.1f;
        temp.jumpCount = 0;

        playerData = temp;
    }
}