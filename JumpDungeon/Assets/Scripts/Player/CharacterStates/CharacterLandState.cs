using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterLandState : State
{
    public override void Enter(Character entity)
    {
        //Logger.Log("Enter Land State");
        
        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.LandParameterHash, true);
        entity.CharacterController.JumpCount = 0;
    }

    public override void Execute(Character entity)
    {
        if (entity.CharacterController.Rigidbody.velocity.y > 0f)
        {
            entity.ChangeState(CharacterStates.Jump);
        }

        if (entity.CharacterController.Rigidbody.velocity.y == 0f)
        {
            entity.ChangeState(CharacterStates.Idle);
        }
    }

    public override void Exit(Character entity)
    {
        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.LandParameterHash, false);
    }
}
