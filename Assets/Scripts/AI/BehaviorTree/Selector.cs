using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
   public class Selector : Node
   {
       public Selector() : base() {}
       public Selector(List<Node> children) : base(children) {}

       public override NodeState Evaluate()
       {
            foreach(Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failed:
                        continue;
                    case NodeState.Running:
                        state = NodeState.Running;
                        return state;
                    case NodeState.Success:
                        state = NodeState.Success;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.Failed; 
            return state;
       }
       
   }
}
