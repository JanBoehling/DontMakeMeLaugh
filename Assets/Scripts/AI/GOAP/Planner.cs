using System.Collections.Generic;

namespace GOAP
{
    public class Planner
    {
        public Queue<Action> Plan(List<Action> actions, Dictionary<string, int> goal, WorldStates worldStates)
        {
            List<Action> achievableActions = new List<Action>();

            foreach (var action in actions)
                if (action.IsAchievable())
                    achievableActions.Add(action);

            List<Node> leaves = new List<Node>();
            Node startNode = new Node(null, 0, worldStates.GetStates(), null);

            if (!BuildGraph(startNode, leaves, achievableActions, goal))
                return null;

            Node cheapestNode = null;
            foreach (var leaf in leaves)
            {
                if (cheapestNode == null)
                    cheapestNode = leaf;
                else
                    if (leaf.Cost < cheapestNode.Cost)
                    cheapestNode = leaf;
            }

            List<Action> result = new List<Action>();
            Node n = cheapestNode;

            while (n != null)
            {
                if (n.Action != null)
                    result.Insert(0, n.Action);
                n = n.Parent;
            }

            Queue<Action> queue = new Queue<Action>();
            foreach (var action in result)
                queue.Enqueue(action);

            return queue;
        }

        private bool BuildGraph(Node parent, List<Node> leaves, List<Action> actions, Dictionary<string, int> goal)
        {
            bool success = false;

            foreach (var action in actions)
            {
                if (!action.IsAchievableGivenPreCons(parent.States))
                    continue;

                Dictionary<string, int> states = new Dictionary<string, int>(parent.States);
                foreach (var effect in action.AfterEffects)
                {
                    if (!states.ContainsKey(effect.Key))
                    {
                        states.Add(effect.Key, effect.Value);
                        continue;
                    }

                    if (states[effect.Key] != effect.Value)
                        states[effect.Key] = effect.Value;
                }

                Node node = new Node(parent, parent.Cost + action.Cost, states, action);

                if (GoalAchieved(goal, states))
                {
                    leaves.Add(node);
                    success = true;
                }
                else
                {
                    List<Action> subset = ActionSubset(actions, action);
                    if (BuildGraph(node, leaves, subset, goal))
                        success = true;
                }
            }

            return success;
        }

        private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> states)
        {
            foreach (var g in goal)
                if (!states.ContainsKey(g.Key))
                    return false;

            return true;
        }

        private List<Action> ActionSubset(List<Action> actions, Action action)
        {
            List<Action> subset = new List<Action>();
            foreach (var act in actions)
                if (act != action)
                    subset.Add(act);

            return subset;
        }
    }
}
