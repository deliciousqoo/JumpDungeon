using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    BoxCollider2D collider;

    private bool getCheck;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        Debug.DrawRay(collider.bounds.center, gameObject.transform.forward, Color.blue, 0.3f);
        RaycastHit2D rayHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, transform.forward, 0.2f, LayerMask.GetMask("Player"));
        if(rayHit.collider != null)
        {
            if (!getCheck) {
                gameObject.GetComponent<Animator>().SetTrigger("IsGetting");
            }
            getCheck = true;
        }
    
    }
}
