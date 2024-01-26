
using System.Collections.Generic;

namespace ProjectBarde.BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base() { }

        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failed:
                        continue;
                    case NodeState.Success:
                        state = NodeState.Success;
                        return state;
                    case NodeState.Running:
                        state = NodeState.Running;
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
