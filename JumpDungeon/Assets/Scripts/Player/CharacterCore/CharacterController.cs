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

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Application.targetFrameRate = 60;
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
        var value = context.ReadValue<Vector2>();

        MoveDirection = value.x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && JumpCount <= 1)
        {
            JumpCount++;
            CharacterJump();
        }
    }

    
}
