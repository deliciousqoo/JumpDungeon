using UnityEngine;

public class CharacterDoublejumpState : State
{
    public override void Enter(Character entity)
    {
        //Logger.Log("Enter Doublejump State");

        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.DoublejumpParameterHash, true);
        entity.CharacterController.JumpCount++;
    }

    public override void Execute(Character entity)
    {
        if(entity.CharacterController.Rigidbody.velocity.y == 0f)
        {
            entity.ChangeState(CharacterStates.Land);
        }

        if(entity.CharacterController.Rigidbody.velocity.y < 0f)
        {
            entity.ChangeState(CharacterStates.Fall);
        }
    }

    public override void Exit(Character entity)
    {
        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.DoublejumpParameterHash, false);
    }
}