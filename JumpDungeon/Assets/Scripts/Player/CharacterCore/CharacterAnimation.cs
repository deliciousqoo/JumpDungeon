using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation
{
    private const string _idleParameterName = "isIdle";
    private const string _runParameterName = "isRun";
    private const string _jumpParameterName = "isJump";
    private const string _doublejumpParameterName = "isDoublejump";
    private const string _fallParameterName = "isFall";
    private const string _landParameterName = "isLand";
    private const string _attackedParameterName = "isAttacked";
    private const string _collapseParameterName = "isCollapse";

    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int DoublejumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }
    public int LandParameterHash { get; private set; }
    public int AttackedParameterHash { get; private set; }
    public int CollapseParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        RunParameterHash = Animator.StringToHash(_runParameterName);
        JumpParameterHash = Animator.StringToHash(_jumpParameterName);
        DoublejumpParameterHash = Animator.StringToHash(_doublejumpParameterName);
        FallParameterHash = Animator.StringToHash(_fallParameterName);
        LandParameterHash = Animator.StringToHash(_landParameterName);
        AttackedParameterHash = Animator.StringToHash(_attackedParameterName);
        CollapseParameterHash = Animator.StringToHash(_collapseParameterName);
    }
}
