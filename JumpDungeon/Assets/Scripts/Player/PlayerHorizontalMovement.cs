using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;

    private Rigidbody2D m_Rigid;

    public bool IsLeftMove { get; set; }
    public bool IsRightMove { get; set; }

    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();    
    }

    private void FixedUpdate()
    {
#if false
        if(IsLeftMove && IsRightMove)
        {
            m_Rigid.velocity = new Vector2(0f, m_Rigid.velocity.y);
        }
        else if(IsLeftMove)
        {
            m_Rigid.AddForce(Vector2.right * -1 * speed, ForceMode2D.Impulse);
            m_Rigid.velocity = new Vector2(Mathf.Clamp(m_Rigid.velocity.x, -1 * maxSpeed, maxSpeed), 0);
        }
        else if(IsRightMove)
        {
            m_Rigid.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
            m_Rigid.velocity = new Vector2(Mathf.Clamp(m_Rigid.velocity.x, -1 * maxSpeed, maxSpeed), 0);
        }
        else
        {
            m_Rigid.velocity = new Vector2(0f, m_Rigid.velocity.y);
        }
#endif
#if DEV_VER
        float h = Input.GetAxisRaw("Horizontal");
        if (InGameManager.Instance.CheckControl)
        {
            //Move Speed
            m_Rigid.AddForce(Vector2.right * h * speed, ForceMode2D.Impulse);
            //Max Speed
            if (m_Rigid.velocity.x > maxSpeed) { m_Rigid.velocity = new Vector2(maxSpeed, m_Rigid.velocity.y); }
            else if (m_Rigid.velocity.x < maxSpeed * (-1)) { m_Rigid.velocity = new Vector2(maxSpeed * (-1), m_Rigid.velocity.y); }
        }
#endif
    }
}
