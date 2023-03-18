using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class CheckCharacterState : Node 
{
   public CheckCharacterState(CharacterStateController ctrlr) : base(ctrlr) {}
   public override NodeState Evaluate()
   {
      if (stateController.MovementState == MovementState.Move)
      {
         state = NodeState.Failed;
         return state;
      }

      state = NodeState.Success;
      return state;
   }
}
