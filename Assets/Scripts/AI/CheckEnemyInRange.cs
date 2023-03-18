using BehaviorTree;
using UnityEngine;


public class CheckEnemyInRange : Node
{
    public CheckEnemyInRange(AIController aiController) : base(aiController) {}
    public override NodeState Evaluate()
    {
        if (aiController.GetDistanceToPlayer() < 2.0f)
        {
            Debug.Log("enemy in range");
            state = NodeState.Success;
            return state;
        }
        
        Debug.Log("enemy out of range");
        state = NodeState.Failed;
        return state;
        
    }
    
}
