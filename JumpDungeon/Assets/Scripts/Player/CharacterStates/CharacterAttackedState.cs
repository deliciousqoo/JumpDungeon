using UnityEngine;

public class CharacterAttackedState : State
{
    public override void Enter(Character entity)
    {
        //Logger.Log("Enter Attacked State");

        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.AttackedParameterHash, true);
    }

    public override void Execute(Character entity)
    {
        
    }

    public override void Exit(Character entity)
    {
        entity.UpdateBoolAnimationParameter(entity.CharacterAnimation.AttackedParameterHash, false);
    }
}