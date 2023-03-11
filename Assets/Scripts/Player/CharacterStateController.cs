using System;
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

public enum ImpactState
{
    None = 0,
    Impact = 1,
}

public class CharacterStateController : MonoBehaviour
{
    public AttackState AttackState { get; set; }
    public JumpState JumpState { get; set; }
    public AttackState WaitingAttackState { get; set; }
    public MovementState MovementState { get; set; }

    public ImpactState ImpactState { get; set; }
}