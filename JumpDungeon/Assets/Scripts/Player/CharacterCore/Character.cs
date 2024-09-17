using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStates { Idle = 0, Run, Jump, DoubleJump, Fall, Land, Attacked, Collapse }

public class Character : BaseGameEntity
{
    public Animator Animator { get; private set; }
    public CharacterAnimation CharacterAnimation { get; private set; }
    public CharacterStateMachine StateMachine { get; private set; }
    public CharacterController CharacterController { get; private set; }

    private State[] _states;
    
    public override void Setup(string name)
    {
        base.Setup(name);
        gameObject.name = name;

        InitializeCharacterSettings();
        InitializeCharacterStates();
    }

    private void InitializeCharacterSettings()
    {
        Animator = GetComponent<Animator>();
        CharacterAnimation = new CharacterAnimation();
        CharacterAnimation?.Initialize();
        CharacterController = GetComponent<CharacterController>();
    }
    private void InitializeCharacterStates()
    {
        _states = new State[8];
        _states[(int)CharacterStates.Idle] = new CharacterIdleState();
        _states[(int)CharacterStates.Run] = new CharacterRunState();
        _states[(int)CharacterStates.Jump] = new CharacterJumpState();
        _states[(int)CharacterStates.DoubleJump] = new CharacterDoublejumpState();
        _states[(int)CharacterStates.Fall] = new CharacterFallState();
        _states[(int)CharacterStates.Land] = new CharacterLandState();
        _states[(int)CharacterStates.Attacked] = new CharacterAttackedState();
        _states[(int)CharacterStates.Collapse] = new CharacterCollapseState();

        StateMachine = new CharacterStateMachine();
        StateMachine.Setup(this, _states[(int)CharacterStates.Idle]);
    }

    public override void Updated()
    {
        StateMachine.Execute();
    }

    public void ChangeState(CharacterStates newState, Action enterAction = null)
    {
        enterAction?.Invoke();

        StateMachine.ChangeState(_states[(int)newState]);
    }

    public void UpdateBoolAnimationParameter(int animationHash, bool value)
    {
        Animator.SetBool(animationHash, value);
    }

    public void UpdateIntegerAnimationParameter(int animationHash, int value)
    {
        Animator.SetInteger(animationHash, value);
    }

    public void StartAnimation(int animationHash)
    {
        Animator.Play(animationHash);
    }
}
