
using System.Collections.Generic;

namespace GOAP
{
    public class Node
    {
        public Node Parent { get; set; }
        public float Cost { get; set; }
        public Dictionary<string, int> States { get; set; }
        public Action Action { get; set; }

        public Node(Node parent, float cost, Dictionary<string, int> states, Action action)
        {
            Parent = parent;
            Cost = cost;
            States = states;
            Action = action;
        }
    }
}
