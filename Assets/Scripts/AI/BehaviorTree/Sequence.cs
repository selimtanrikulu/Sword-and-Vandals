using System.Collections.Generic;

namespace BehaviorTree
{
   public class Sequence : Node
   {
       public Sequence() : base() {}
       public Sequence(List<Node> children) : base(children) {}

       public override NodeState Evaluate()
       {
            bool anyChildIsRunning = false;
            foreach(Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failed:
                        state = NodeState.Failed;
                        return state;
                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;
                    case NodeState.Success:
                        continue;
                    default:
                        state = NodeState.Success;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.Running : NodeState.Success;
            return state;
       }
       
   }
}