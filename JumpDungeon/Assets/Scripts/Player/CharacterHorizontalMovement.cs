using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterHorizontalMovement : MonoBehaviour
{
    public bool CheckControl { get; set; } = true;
    public float speed;
    public float maxSpeed;

    private Rigidbody2D m_Rigid;


    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if(CheckControl)
        {
            m_Rigid.AddForce(Vector2.right * h * speed, ForceMode2D.Impulse);
            m_Rigid.velocity = new Vector2(Mathf.Clamp(m_Rigid.velocity.x, -1 * maxSpeed, maxSpeed), 0);
        }
    }
}
