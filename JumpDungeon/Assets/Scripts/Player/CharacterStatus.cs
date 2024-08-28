using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus
{
    public enum CharacterConditions
    {
        Normal,
        ControlledMovement,
    }

    public enum MovementStates
    {
        Null,
        Idle,
        Running,
        Jumping,
        Falling,
        DoubleJumping,
    }
}
