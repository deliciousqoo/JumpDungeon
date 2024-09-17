using UnityEngine;

public class CharacterJumpState : State
{
    public override void Enter(Character entity)
    {
        //Logger.Log("Enter Jump State");

        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.JumpParameterHash, true);
        entity.StartAnimation(entity.CharacterAnimation.JumpParameterHash);
    }

    public override void Execute(Character entity)
    {
        if(entity.CharacterController.JumpCount == 2)
        {
            entity.ChangeState(CharacterStates.DoubleJump);
        }

        if(entity.CharacterController.Rigidbody.velocity.y == 0f)
        {
            entity.ChangeState(CharacterStates.Land);
        }
    }

    public override void Exit(Character entity)
    {
        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.JumpParameterHash, false);
    }
}