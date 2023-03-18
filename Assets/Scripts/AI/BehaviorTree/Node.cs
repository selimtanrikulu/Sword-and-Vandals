using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace BehaviorTree
{
    
   public enum NodeState
   {
      Success,
      Running,
      Failed
   }

   public class Node  
   {
      protected NodeState state;
      public Node parent;
      protected List<Node> children = new List<Node>();
      protected AIController aiController;
      protected CharacterStateController stateController;

      public Node()
      {
         parent = null;
      }
      public Node(AIController aiController)
      {
         this.aiController = aiController;
      }

      public Node(CharacterStateController chsc)
      {
         stateController = chsc;
      }

      public Node(List<Node> children)
      {
         foreach (Node child in children)
         {
            child.parent = this;
            this.children.Add(child);
         }
      }
      
      public virtual NodeState Evaluate() => NodeState.Failed;
   }
   
}
