using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequenzer : Node
    {
        public Sequenzer() : base() { }

        public Sequenzer(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool isAnyChildRunning = false;

            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failed:
                        state = NodeState.Failed;
                        return state;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        continue;
                    default:
                        state = NodeState.Success;
                        return state;
                }
            }

            state = isAnyChildRunning ? NodeState.Running : NodeState.Success;
            return state;
        }
    }
}
