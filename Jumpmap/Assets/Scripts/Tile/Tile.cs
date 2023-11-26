using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private PlayerMovement player;
    
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ãæµ¹");
    }*/
}
