using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerInteraction : MonoBehaviour
{
    public PlayerAffectedValue playerData;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    private Animator anim;
    
    [SerializeField]
    private GameObject damagedPrefab, shieldBreakPrefab, shield;
    
    private Coroutine damageCoroutine, completeCoroutine, shieldCoroutine;

    private bool checkControl = true;
    private int jumpCount, dirc;
    public bool previous_status = false;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        if (GameManager.instance.shieldCheck)
        {
            shield.SetActive(true);
            gameObject.layer = 14;
        }
        else
        {
            shield.SetActive(false);
            gameObject.layer = 10;
        }
        InitPlayerValue();
    }
    private void FixedUpdate()
    {
        Collider2D checkHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.015f, LayerMask.GetMask("Platform")).collider;
        if (checkHit != null && checkHit.tag == "Tile")
        {
            switch(checkHit.gameObject.name.Substring(0,5))
            {
                case "Water":
                    checkHit.gameObject.GetComponent<Water>().InitAffectValue();
                    playerData = checkHit.gameObject.GetComponent<Water>().playerData;
                    previous_status = false;
                    break;
                case "Waves":
                    checkHit.gameObject.GetComponent<Waves>().InitAffectValue();
                    checkHit.gameObject.GetComponent<Waves>().AddForceCall(gameObject);
                    playerData = checkHit.gameObject.GetComponent<Waves>().playerData;
                    previous_status = false;
                    break;
                case "Swamp":
                    checkHit.gameObject.GetComponent<Swamp>().InitAffectValue();
                    playerData = checkHit.gameObject.GetComponent<Swamp>().playerData;
                    previous_status = false;
                    break;
                case "Cloud":
                    InitPlayerValue();
                    checkHit.gameObject.GetComponent<Cloud>().AddForceCall(gameObject);
                    playerData.jumpCount = 0;
                    break;
                default:
                    InitPlayerValue();
                    break;
            }
            GetComponent<PlayerMovement>().SetPlayerData(playerData);
        }
        else
        {
            if (!previous_status)
            {
                Debug.Log("check");
                InitPlayerValue();
                GetComponent<PlayerMovement>().SetPlayerData(playerData);
            }
            previous_status = true;
        }
    }
    private void InitPlayerValue()
    {
        playerData.jumpCount = 1;
        playerData.jumpPower = 3f;
        playerData.gravityScale = 1.0f;
        playerData.drag = 3f;
        playerData.speed = 1f;
        playerData.maxSpeed = 0.7f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (GameManager.instance.shieldCheck)
            {
                if (shieldCoroutine == null) { shieldCoroutine = StartCoroutine("OnShield"); }
            }
            else
            {
                if (damageCoroutine != null) { StopCoroutine(damageCoroutine); }
                damageCoroutine = StartCoroutine(OnDamaged(collision.transform.position));

                GameObject damagedEffect = Instantiate(damagedPrefab);
                damagedEffect.GetComponent<Transform>().position = gameObject.transform.position;
            }
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

        if (targetPos.x - gameObject.transform.position.x > 0) { dirc = 1; }
        else { dirc = -1; }
        if (dirc == 1) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;

        while (Mathf.Abs(targetPos.x - gameObject.transform.position.x) >= 0.01f)
        {
            Debug.Log(Mathf.Abs(targetPos.x - gameObject.transform.position.x));
            tempPos.x += 0.002f * dirc;
            gameObject.transform.position = tempPos;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        anim.Play("Idle");

        yield return 0;
    }
    public IEnumerator OnShield()
    {
        GameObject shieldBreakEffect = Instantiate(shieldBreakPrefab);
        shieldBreakEffect.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, 0, 0);
        shield.SetActive(false);

        Color tempColor = spriteRenderer.color;
        for (int i = 0; i < 2; i++)
        {
            while (tempColor.a > 0.1)
            {
                tempColor.a -= 0.02f;
                spriteRenderer.color = tempColor;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            while (tempColor.a < 0.8)
            {
                tempColor.a += 0.02f;
                spriteRenderer.color = tempColor;
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
        tempColor.a = 1;
        spriteRenderer.color = tempColor;

        GameManager.instance.shieldCheck = false;
        gameObject.layer = 14;
    }
}
