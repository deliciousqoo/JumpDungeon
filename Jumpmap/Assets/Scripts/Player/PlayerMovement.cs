using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    private Animator anim;

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
            if (Input.GetButtonDown("Jump") && anim.GetInteger("jumpCount") < 2)
            {
                //Debug.Log("jump");
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.AddForce(Vector2.up * playerData.jumpPower, ForceMode2D.Impulse);
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

        RaycastHit2D checkHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.015f, LayerMask.GetMask("Platform"));
        if (checkControl)
        {
            //Move Speed
            rigid.AddForce(Vector2.right * h * playerData.speed, ForceMode2D.Impulse);

            //Max Speed
            if (rigid.velocity.x > playerData.maxSpeed) { rigid.velocity = new Vector2(playerData.maxSpeed, rigid.velocity.y); }
            else if (rigid.velocity.x < playerData.maxSpeed * (-1)) { rigid.velocity = new Vector2(playerData.maxSpeed * (-1), rigid.velocity.y); }
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

        //Check Gimmick Tile
        if (checkHit.collider != null && checkHit.collider.tag == "Tile")
        {
            if (checkHit.collider.gameObject.name.Substring(0, 5) == "Water")
            {
                checkHit.collider.gameObject.GetComponent<Animator>().Play("Water_Move");
                rigid.gravityScale = 0.3f;
                rigid.drag = 10f;
                jumpCount = 0;
                anim.SetInteger("jumpCount", 0);
            }
            else if (checkHit.collider.gameObject.name.Substring(0, 4) == "Wave")
            {
                jumpCount = 0;
                anim.Play("Falling");
                anim.SetInteger("jumpCount", 0);
                rigid.gravityScale = 0f;
                rigid.drag = 10f;
                switch (checkHit.collider.gameObject.name[4])
                {
                    case 'R':
                        rigid.AddForce(Vector2.right * 30f);
                        break;
                    case 'L':
                        rigid.AddForce(Vector2.left * 30f);
                        break;
                    case 'U':
                        rigid.AddForce(Vector2.up * 20f);
                        break;
                    case 'D':
                        rigid.AddForce(Vector2.down * 20f);
                        break;
                }
            }
            else if (checkHit.collider.gameObject.name.Substring(0, 5) == "Swamp")
            {
                rigid.gravityScale = 0.1f;
                rigid.drag = 50f;
                jumpCount = 0;
                playerData.jumpPower = 1.5f;
                anim.SetInteger("jumpCount", 0);
                playerData.speed = 0.1f;
            }
            else if (checkHit.collider.gameObject.name.Substring(0, 5) == "Cloud")
            {
                StartCoroutine("OnCloudTile");
            }
            previous_status = false;
        }
        else
        {
            if (!previous_status)
            {
                jumpCount = 1;
                playerData.jumpPower = 3f;
            }
            rigid.gravityScale = 1.0f;
            rigid.drag = 3f;
            previous_status = true;
            playerData.speed = 1f;
        }
    }
}
