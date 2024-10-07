using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public float MaxSpeed;
    public float MovementSpeed;
    public float JumpPower;
    public float MoveDirection;
    public int JumpCount;

    public Rigidbody2D Rigidbody { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    private Coroutine _damageCoroutine;
    private bool _checkControl = true;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Application.targetFrameRate = 60;
        if (!_checkControl) return;

        CharacterHorizontalMovement();
        CheckDirection();
    }

    private void CharacterHorizontalMovement()
    {
        Rigidbody.AddForce(Vector2.right * MoveDirection * MovementSpeed, ForceMode2D.Impulse);
        ClampSpeed();
    }

    private void CheckDirection()
    {
        if (MoveDirection == -1) SpriteRenderer.flipX = true;
        else if(MoveDirection == 1) SpriteRenderer.flipX = false;
    }

    private void CharacterJump()
    {
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0f);
        Rigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
    }

    private void ClampSpeed()
    {
        Rigidbody.velocity = new Vector2(Mathf.Clamp(Rigidbody.velocity.x, (-1) * MaxSpeed, MaxSpeed), Rigidbody.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!_checkControl) return;

        var value = context.ReadValue<Vector2>();

        MoveDirection = value.x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!_checkControl) return;

        if (context.performed && JumpCount <= 1)
        {
            JumpCount++;
            CharacterJump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            
            if (_damageCoroutine != null) { StopCoroutine(_damageCoroutine); }
            _damageCoroutine = StartCoroutine(OnDamaged(collision.transform.position));
        }
    }

    private IEnumerator OnDamaged(Vector2 targetPos)
    {
        _checkControl = false;
        gameObject.GetComponent<Character>().ChangeState(CharacterStates.Attacked);
        SpriteRenderer.color = new Color(1, 1, 1, 0.4f);

        MoveDirection = transform.position.x - targetPos.x > 0 ? 1 : -1;
        Rigidbody.AddForce(new Vector2(MoveDirection, 0.5f) * 2f, ForceMode2D.Impulse);

        if (MoveDirection == 1) SpriteRenderer.flipX = true;
        else SpriteRenderer.flipX = false;

        yield return new WaitUntil(() => Rigidbody.velocity.y == 0);
        GetComponent<Character>().ChangeState(CharacterStates.Collapse);
        yield return new WaitForSecondsRealtime(1f);

        GetComponent<Character>().ChangeState(CharacterStates.Idle);
        SpriteRenderer.color = new Color(1, 1, 1, 1);

        _checkControl = true;
        _damageCoroutine = null;
        MoveDirection = 0;
    }
}
