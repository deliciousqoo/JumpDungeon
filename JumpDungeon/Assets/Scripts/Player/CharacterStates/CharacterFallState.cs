using UnityEngine;

public class CharacterFallState : State
{
    public override void Enter(Character entity)
    {
        //Logger.Log("Enter Fall State");

        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.FallParameterHash, true);
    }

    public override void Execute(Character entity)
    {
        if (entity.CharacterController.Rigidbody.velocity.y == 0)
        {
            entity.ChangeState(CharacterStates.Land);
        }

        if (entity.CharacterController.JumpCount == 2)
        {
            entity.ChangeState(CharacterStates.DoubleJump);
        }
    }

    public override void Exit(Character entity)
    {
        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.FallParameterHash, false);
    }
}