using BehaviorTree;
using UnityEngine;


public class DodgeAttack : Node 
{
    public DodgeAttack(AIController aiController): base(aiController){}
    public override NodeState Evaluate()
    {
        Debug.Log("dodge attack");
        aiController.Vertical = -1.0f;
        aiController.Horizontal = 0.0f;
        aiController.RollInput = true;
        
        state = NodeState.Success;
        return state;
    }
}
public class BlockAttack : Node 
{
    public BlockAttack(AIController aiController): base(aiController){}
    public override NodeState Evaluate()
    {
        Debug.Log("block attack");
        aiController.Vertical = -1.0f;
        aiController.BlockInput = true;
        state = NodeState.Success;
        return state;
    }
}
