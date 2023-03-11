using UnityEngine;


public enum MovementState
{
    //Includes idle
    Move = 0,
    RollForward = 1,
    RollBackward = 2,
    RollRight = 3,
    RollLeft = 4,
    RollForwardLeft = 5,
    RollForwardRight = 6,
    RollBackwardLeft = 7,
    RollBackwardRight = 8,
    Stunned = -1
}

public enum AttackState
{
    None = 0,
    Triple1 = 31,
    Triple2 = 32,
    Triple3 = 33,
    Double1 = 21,
    Double2 = 22,
    Single = 1,
    Block = -1,
}


public enum JumpState
{
    Grounded = 0,
    JumpStart = 1,
    Fall = 2,
}



public class CharacterStateController : MonoBehaviour
{
    public AttackState AttackState { get; set; }
    public JumpState JumpState { get; set; }
    public AttackState WaitingAttackState { get; set; }
    public MovementState MovementState { get; set; }

}