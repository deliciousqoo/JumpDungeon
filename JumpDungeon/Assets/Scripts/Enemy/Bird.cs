using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy, IAttackable
{
    public float DetectionRange { get; set; }
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Detection()
    {
        throw new System.NotImplementedException();
    }
}
