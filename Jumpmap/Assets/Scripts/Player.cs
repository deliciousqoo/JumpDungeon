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

    public bool jumpCheck;
    public int jumpCount;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && anim.GetInteger("jumpCount") < 2) {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);

            jumpCount++;
            anim.SetInteger("jumpCount", jumpCount);
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

        //Falling
        if(rigid.velocity.y < 0) {
            anim.SetBool("isFalling", true);
            if (jumpCount == 2) anim.SetInteger("jumpCount", 0);
        }
        else {
            anim.SetBool("isFalling", false);
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
        if(rigid.velocity.y == 0) {
            anim.SetBool("isJumping", false);
            jumpCount = 0;
            anim.SetInteger("jumpCount", jumpCount);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
        
    }
}
