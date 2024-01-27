using ProjectBarde.BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class StonePaperSissorBehaviour : BehaviorTreeBase
{
    [SerializeField]
    private int _stateCount = 3;

    protected override Node SetupTree()
    {
        Node root = new Sequenzer(
            new List<Node>()
            {
                new TaskDrawSymbol(_stateCount),

            }
        );

        return root;
    }
}
