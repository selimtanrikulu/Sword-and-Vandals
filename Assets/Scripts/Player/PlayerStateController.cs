using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MovementState
{
    //Includes idle
    Move = 0,
    RollForward = 1,
    RollBackward = 2,
    RollRight = 3,
    RollLeft = 4,
    Stunned = 5,
}

public enum AttackState
{
    None = 0,
    Attack1_1 = 11,
    Attack1_2 = 12,
    Attack1_3 = 13,
    Attack2 = 3,
    Block = -1,
}


public enum JumpState
{
    Grounded = 0,
    JumpStart = 1,
    Fall = 2,
}


[Serializable]
public enum DodgeType
{
    Forward,
    Backward,
    Left,
    Right,
}

public class PlayerStateController : MonoBehaviour
{
    public AttackState attackState { get; set; }
    public JumpState jumpState { get; set; }
    public AttackState waitingAttackState { get; set; }
    public MovementState movementState { get; set; }
}