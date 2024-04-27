using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpPower, speed, maxSpeed;
    public int jumpCount;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    private Animator anim;

    private bool checkControl = true;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (checkControl)
        {
            //Jump
            if (Input.GetButtonDown("Jump") && jumpCount < 2)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isJumping", true);

                jumpCount++;
                anim.SetInteger("jumpCount", jumpCount);
            }

            //Stop Speed
            if (Input.GetButtonUp("Horizontal")) { rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.6f, rigid.velocity.y); }
            else if (Input.GetButton("Right") && Input.GetButton("Left")) { rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0f, rigid.velocity.y); }

            //Direction Sprite
            if (Input.GetButtonDown("Left")) { spriteRenderer.flipX = true; collider.offset = new Vector2(0.005f, -0.0275f); }
            else if (Input.GetButtonDown("Right")) { spriteRenderer.flipX = false; collider.offset = new Vector2(-0.005f, -0.0275f); }
        }

        //Animation
        if (rigid.velocity.normalized.x == 0) { anim.SetBool("isRunning", false); }
        else { anim.SetBool("isRunning", true); }

        //Falling
        if (anim.GetBool("isJumping"))
        {
            if (rigid.velocity.y < 0)
            {
                anim.SetBool("isFalling", true);
                if (jumpCount == 2)
                {
                    jumpCount++;
                    anim.SetInteger("jumpCount", jumpCount);
                }
            }
            else { anim.SetBool("isFalling", false); }
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (checkControl)
        {
            //Move Speed
            rigid.AddForce(Vector2.right * h * speed, ForceMode2D.Impulse);
            //Max Speed
            if (rigid.velocity.x > maxSpeed) { rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); }
            else if (rigid.velocity.x < maxSpeed * (-1)) { rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y); }
        }

        //Landing Platform
        if (rigid.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            RaycastHit2D rayHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Platform"));
            if ((rayHit.collider != null && rayHit.distance < 0.015f) || jumpCount == 3)
            {
                jumpCount = 0;
            }
            anim.SetInteger("jumpCount", jumpCount);
        }
        else { anim.SetBool("isJumping", true); }
    }

    public void SetPlayerData(PlayerAffectedValue setValue)
    {
        maxSpeed = setValue.maxSpeed;
        jumpPower = setValue.jumpPower;
        speed = setValue.speed;
        rigid.drag = setValue.drag;
        rigid.gravityScale = setValue.gravityScale;
        jumpCount = setValue.jumpCount;
        anim.SetInteger("jumpCount", setValue.jumpCount);
    }
}
