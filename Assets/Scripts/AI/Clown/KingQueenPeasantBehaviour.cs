using ProjectBarde.BehaviorTree;
using System.Collections.Generic;

public class KingQueenPeasantBehaviour : BehaviorTreeBase
{
    protected override Node SetupTree()
    {
        Node root = new Sequenzer(new List<Node>()
        {

        });
        return root;
    }
}
