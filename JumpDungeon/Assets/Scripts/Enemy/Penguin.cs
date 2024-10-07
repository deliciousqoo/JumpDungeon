using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Enemy, IMovable, IAttackable
{
    public float Speed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int Direction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float DetectionRange { get; set; }
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeDirection()
    {
        throw new System.NotImplementedException();
    }

    public void Detection()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }
}
