using BehaviorTree;
using UnityEngine;


public class Attack : Node
{
    public Attack(AIController aiController): base(aiController) {}
    public override NodeState Evaluate()
    {
        Debug.Log("attacking enemy");
        aiController.BasicAttackInput = true;
        state = NodeState.Success;
        return state;
    }
}
