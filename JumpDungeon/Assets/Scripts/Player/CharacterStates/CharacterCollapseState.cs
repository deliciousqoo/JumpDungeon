using UnityEngine;

public class CharacterCollapseState : State
{
    public override void Enter(Character entity)
    {
        //Logger.Log("Enter Collapse State");

        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.CollapseParameterHash, true);
        entity.CharacterController.JumpCount = 0;
    }

    public override void Execute(Character entity)
    {
        
    }

    public override void Exit(Character entity)
    {
        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.CollapseParameterHash, false);
    }
}