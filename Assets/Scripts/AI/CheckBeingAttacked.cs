using BehaviorTree;
using UnityEngine;

public class CheckBeingAttacked : Node 
{
    public CheckBeingAttacked(AIController aiController) : base(aiController) {}
    public override NodeState Evaluate()
    {
        AttackState enemyAttackState = aiController.enemy.AttackState;
        if (enemyAttackState != AttackState.None)
        {
            Debug.Log("being attacked");
            state = NodeState.Success;
            return state;
        }
        state = NodeState.Failed;
        return state;
    }
    
}

