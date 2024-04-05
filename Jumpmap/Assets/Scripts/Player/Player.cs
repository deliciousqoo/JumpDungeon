using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental;
using UnityEngine.U2D.Animation;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float speed;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;
    Animator anim;
    SpriteLibrary spriteLibrary;

    private Coroutine damageCoroutine;
    private Coroutine completeCoroutine;

    private bool checkControl = true;
    private int jumpCount, dirc;
    private bool previous_status = false;

    [SerializeField]
    private GameObject damagedPrefab;

    [SerializeField]
    private SpriteLibraryAsset[] skinList;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteLibrary = GetComponent<SpriteLibrary>();
    }

    private void Start()
    {
        spriteLibrary.spriteLibraryAsset = skinList[GameManager.instance.skinNum];
    }

    //Setter
    public void SetPlayerJumpCount(int count) { this.jumpCount = count; }

    //Getter
    public int GetPlayerJumpCount() { return jumpCount; }

    //Function
    private void Update()
    {
        if (checkControl)
        {
            //Jump
            if (Input.GetButtonDown("Jump") && anim.GetInteger("jumpCount") < 2)
            {
                //Debug.Log("jump");
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

        RaycastHit2D checkHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.zero, 0.2f, LayerMask.GetMask("Platform"));
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
            RaycastHit2D rayHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.2f, LayerMask.GetMask("Platform"));
            if ((rayHit.collider != null && rayHit.distance < 0.015f) || jumpCount == 3) { 
                jumpCount = 0;
            }
            anim.SetInteger("jumpCount", jumpCount);
        }
        else { anim.SetBool("isJumping", true); }

        //Check Gimmick
        if(checkHit.collider != null)
        {
            //Debug.Log(checkHit.collider.gameObject.name);
            if(checkHit.collider.gameObject.name.Substring(0,5) == "Water")
            {
                checkHit.collider.gameObject.GetComponent<Animator>().Play("Water_Move");
                rigid.gravityScale = 0.3f;
                rigid.drag = 10f;
                jumpCount = 0;
                anim.SetInteger("jumpCount", 0);
            }
            else if(checkHit.collider.gameObject.name.Substring(0, 4) == "Wave")
            {
                rigid.gravityScale = 3f;
            }
            else if(checkHit.collider.gameObject.name.Substring(0, 5) == "Swamp")
            {
                rigid.gravityScale = 0.1f;
                rigid.drag = 50f;
                jumpCount = 0;
                jumpPower = 1.5f;
                anim.SetInteger("jumpCount", 0);
                speed = 0.1f;
            }
            previous_status = false;
        }
        else
        {
            if (!previous_status)
            {
                jumpCount = 1;
                jumpPower = 3f;
            }
            rigid.gravityScale = 1.0f;
            rigid.drag = 3f;
            previous_status = true;
            speed = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Attacked");
            if (damageCoroutine != null) { StopCoroutine(damageCoroutine); }
            damageCoroutine = StartCoroutine(OnDamaged(collision.transform.position));

            GameObject damagedEffect = Instantiate(damagedPrefab);
            damagedEffect.GetComponent<Transform>().position = gameObject.transform.position;
        }
    }

    public void OnCompletedCall(Vector2 targetPos)
    {
        if (completeCoroutine != null) { StopCoroutine(completeCoroutine); }
        completeCoroutine = StartCoroutine(OnCompleted(targetPos));
    }

    public IEnumerator OnDamaged(Vector2 targetPos)
    {
        //Animation Control
        anim.Play("Attacked");

        //Block Input
        checkControl = false;

        //Attacked Change Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Player Pushing
        dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 0.5f) * 2f, ForceMode2D.Impulse);

        //Change Flip Direction
        if (dirc == 1) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;

        yield return new WaitUntil(() => rigid.velocity.y == 0);
        anim.Play("Collapse");
        yield return new WaitForSecondsRealtime(1f);

        //Return Origin State
        anim.Play("Idle");
        spriteRenderer.color = new Color(1, 1, 1, 1);

        checkControl = true;

        damageCoroutine = null;
    }
    
    public IEnumerator OnCompleted(Vector2 targetPos)
    {
        checkControl = false;
        yield return new WaitUntil(() => rigid.velocity.y == 0);

        Vector2 tempPos = gameObject.transform.position;
        Color tempColor = spriteRenderer.color;
        
        anim.Play("Complete");
        rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
        //rigid.bodyType = RigidbodyType2D.Static;

        if(targetPos.x - gameObject.transform.position.x > 0) { dirc = 1; }
        else { dirc = -1; }
        if (dirc == 1) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;

        while(Mathf.Abs(targetPos.x - gameObject.transform.position.x) >= 0.01f)
        {
            Debug.Log(Mathf.Abs(targetPos.x - gameObject.transform.position.x));
            tempPos.x += 0.002f * dirc;
            gameObject.transform.position = tempPos;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        anim.Play("Idle");

        /*while (tempColor.a > 0)
        {
            tempColor.a -= 0.03f;
            gameObject.GetComponent<SpriteRenderer>().color = tempColor;
            yield return new WaitForSecondsRealtime(0.03f);
        }*/

        //SceneManager.LoadScene(0);
        //yield return new WaitForSecondsRealtime(0.5f);
        

        yield return 0;
    }

    public void PlayerHide()
    {
        Color tempColor = spriteRenderer.color;
        tempColor.a = 0f;
        spriteRenderer.color = tempColor;
    }
}
