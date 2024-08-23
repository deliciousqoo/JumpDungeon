using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float JumpPower;
    public int JumpCount;

    private Rigidbody2D m_Rigid;

    public void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
    } 
#if false
    public void DoPlayerJump()
    {
        m_Rigid.velocity = new Vector2(m_Rigid.velocity.x, 0f);
        m_Rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        
        return;
    }
#endif
#if DEV_VER
    private void Update()
    {
        if (InGameManager.Instance.CheckControl)
        {
            if (Input.GetButtonDown("Jump"))
            {
                m_Rigid.velocity = new Vector2(m_Rigid.velocity.x, 0);
                m_Rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            }
        }
    }
#endif
}
