using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump")) { 
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }


        //Stop Speed
        if (Input.GetButtonUp("Horizontal") || (Input.GetButton("Right") && Input.GetButton("Left"))) { 
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0f, rigid.velocity.y); 
        }

        //Direction Sprite
        if (Input.GetButtonDown("Left")) {
            spriteRenderer.flipX = true;
        }
        else if (Input.GetButtonDown("Right")) {
            spriteRenderer.flipX = false;
        }
    
        //Animation
        if(rigid.velocity.normalized.x == 0) { 
            anim.SetBool("isRunning", false); 
        }
        else { 
            anim.SetBool("isRunning", true); 
        }
    }

    private void FixedUpdate()
    {
        //Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max Speed
        if(rigid.velocity.x > maxSpeed) { 
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); 
        }
        else if(rigid.velocity.x < maxSpeed*(-1)) { 
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y); 
        }

        //Landing Platform
        if(rigid.velocity.y < 0) {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.11f)
                {
                    anim.SetBool("isJumping", false);
                }
            }
        }
        
    }
}
