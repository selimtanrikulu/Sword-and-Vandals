using System.Collections.Generic;
using BehaviorTree;
using Sequence = BehaviorTree.Sequence;
using Tree = BehaviorTree.Tree;

public class AIBehaviorTree : Tree
{
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            // new CheckCharacterState(characterStateController),
            new Sequence(new List<Node>
            {
               new CheckEnemyInRange(aiController),
               new CheckBeingAttacked(aiController),
               new Selector( new List<Node>
               {
                  new DodgeAttack(aiController),
                  new BlockAttack(aiController)
               }),
            }),
            new Attack(aiController),
            new TaskChasePlayer(aiController),
        });
        return root;
    }
}
