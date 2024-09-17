using UnityEngine;

public class CharacterIdleState : State
{
    public override void Enter(Character entity)
    {
        //Logger.Log("Enter Idle State");

        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.IdleParameterHash, true);
    }

    public override void Execute(Character entity)
    {
        if (entity.CharacterController.Rigidbody.velocity.x != 0f)
        {
            entity.ChangeState(CharacterStates.Run);
        }

        if(entity.CharacterController.Rigidbody.velocity.y > 0f)
        {
            entity.ChangeState(CharacterStates.Jump);
        }

        if(entity.CharacterController.Rigidbody.velocity.y < 0f)
        {
            entity.ChangeState(CharacterStates.Fall);
        }
    }

    public override void Exit(Character entity)
    {
        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.IdleParameterHash, false);
    }
}
