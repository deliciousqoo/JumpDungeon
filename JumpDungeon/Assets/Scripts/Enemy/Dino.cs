using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : Enemy, IMovable, IShootable, IAttackable
{
    public GameObject ProjectilePrefab;

    public Vector2 StartPos;
    public Vector2 EndPos;

    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float ProjectileSpeed { get; set; }
    [field: SerializeField] public float ShootCycle { get; set; }
    [field: SerializeField] public float ShootRange { get; set; }
    [field: SerializeField] public float DetectionRange { get; set; }
    public int Direction { get; set; }

    private bool _isShooting = false;

    private void Start()
    {
        transform.position = StartPos;
        Direction = StartPos.x < EndPos.x ? 1 : -1;

        InitializeInfo();
    }

    protected override void InitializeInfo()
    {
        base.InitializeInfo();

        _collider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        if (!_isShooting)
        {
            ChangeDirection();
            Detection();
        }
    }

    public void ChangeDirection()
    {
        if (Direction == 1 && transform.position.x >= Mathf.Max(StartPos.x, EndPos.x))
        {
            _spriteRenderer.flipX = false;
            Direction = -1;
        }
        else if (Direction == -1 && transform.position.x <= Mathf.Min(StartPos.x, EndPos.x))
        {
            _spriteRenderer.flipX = true;
            Direction = 1;
        }
    }

    public void Move()
    {
        Vector2 currentPosition = transform.position;
        currentPosition.x += Speed * Direction * Time.fixedDeltaTime;
        transform.position = currentPosition;
    }

    public void Shoot()
    {
        GameObject clone = Instantiate(ProjectilePrefab, gameObject.transform);
        clone.transform.localPosition = new Vector3(0f, 0f, 0f);

        clone.GetComponent<DinoProjectile>().Setup(ProjectileSpeed, Direction, ShootRange);
    }

    public void Detection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * Direction, DetectionRange);
        Debug.DrawRay(transform.position, Vector2.right * Direction * DetectionRange, Color.red);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            StartCoroutine(ShootProjectile());
        }
        else
        {
            Move();
        }
    }

    public IEnumerator ShootProjectile()
    {
        _isShooting = true;
        yield return new WaitForSeconds(0.5f);

        Shoot();

        yield return new WaitForSeconds(ShootCycle);
        _isShooting = false;
    }

    public void Attack()
    {

    }
}
