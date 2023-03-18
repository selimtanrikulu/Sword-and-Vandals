using BehaviorTree;
using UnityEngine;

public class TaskChasePlayer : Node
{
    public TaskChasePlayer(AIController aiController): base(aiController) {}
    public override NodeState Evaluate()
    {
        Debug.Log("chasing enemy");
        aiController.Vertical = 0.5f;
        
        state = NodeState.Running;
        return state;
    }
}
