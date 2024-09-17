using UnityEngine;

public class CharacterRunState : State
{
    public override void Enter(Character entity)
    {
        //Logger.Log("Enter Run State");

        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.RunParameterHash, true);
    }

    public override void Execute(Character entity)
    {
        if (entity.CharacterController.Rigidbody.velocity.x == 0)
        {
            entity.ChangeState(CharacterStates.Idle);
        }

        if (entity.CharacterController.Rigidbody.velocity.y > 0)
        {
            entity.ChangeState(CharacterStates.Jump);
        }
    }

    public override void Exit(Character entity)
    {
        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.RunParameterHash, false);
    }
}