using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseGameEntity : MonoBehaviour
{
    private string entityName;
    private string personalColor;

    public virtual void Setup(string name)
    {
        entityName = name;

        int color = Random.Range(0, 1000000);
        personalColor = $"#{color.ToString("X6")}";
    }

    public abstract void Updated();
}
