using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy, IAttackable
{
    [field: SerializeField] public float DetectionRange { get; set; }
    [field: SerializeField] public float JumpForce { get; set; }
    [field: SerializeField] public float JumpAngle { get; set; }
    [field: SerializeField] public float JumpDelay { get; set; }

    private Transform player;
    private bool isJumping = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        InitializeInfo();
    }

    protected override void InitializeInfo()
    {
        base.InitializeInfo();

        _rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Detection();
    }

    public void Detection()
    {
        Collider2D hit = Physics2D.OverlapCircle(_collider.bounds.center, DetectionRange, LayerMask.GetMask("Player"));

        if (hit != null && hit.CompareTag("Player") && !isJumping)
        {
            Vector2 directionToPlayer = (hit.transform.position - transform.position).normalized;

            if (directionToPlayer.x > 0)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }

            StartCoroutine("Jump");
        }
    }

    private IEnumerator Jump()
    {
        isJumping = true;
        yield return new WaitForSeconds(JumpDelay);

        Attack();

        yield return new WaitForSeconds(1f);
        isJumping = false;
    }

    public void Attack()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        float jumpAngleRad = JumpAngle * Mathf.Deg2Rad;
        Vector2 jumpDirection = new Vector2(directionToPlayer.x, Mathf.Tan(jumpAngleRad));

        _rigid.AddForce(jumpDirection.normalized * JumpForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_collider.bounds.center, DetectionRange);
    }
}
