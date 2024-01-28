using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

namespace GOAP
{
    public class Agent : MonoBehaviour
    {
        public List<Action> Actions = new List<Action>();
        public Dictionary<SubGoal, int> Goals = new Dictionary<SubGoal, int>();
        public WorldStates AgentBeliefs;

        private Planner _planner;
        private Queue<Action> _actionQueue;
        private Action _currentAction;
        private SubGoal _currentGoal;
        private bool _actionInvoked;

        protected virtual void Start()
        {
            Action[] actions = GetComponents<Action>();
            foreach (Action action in actions)
                Actions.Add(action);
        }

        public void NextStep()
        {
            while (!_actionInvoked)
            {
                if (_currentAction != null && _currentAction.Running)
                {
                    if (!_actionInvoked)
                    {
                        Invoke(nameof(CompleteAction), _currentAction.Duration);
                        _actionInvoked = true;
                        return;
                    }

                    continue;
                }

                if (_planner == null || _actionQueue == null)
                {
                    _planner = new Planner();
                    var sortedGoals = from entry in Goals orderby entry.Value descending select entry;
                    WorldStates states = new WorldStates();
                    foreach (var state in World.Instance.GetWorld().GetStates())
                        states.ModifyState(state.Key, state.Value);

                    foreach (var state in AgentBeliefs.GetStates())
                        states.ModifyState(state.Key, state.Value);

                    foreach (var action in Actions)
                        foreach (var state in action.Beliefs.GetStates())
                            states.ModifyState(state.Key, state.Value);

                    foreach (var subGoal in sortedGoals)
                    {
                        _actionQueue = _planner.Plan(Actions, subGoal.Key.Goals, states);
                        if (_actionQueue != null)
                        {
                            _currentGoal = subGoal.Key;
                            break;
                        }
                    }

                    if (_actionQueue == null)
                    {
                        _actionQueue = new Queue<Action>();
                        _actionQueue.Enqueue(PlayNextPosibleCard(states));
                    }
                }

                if (_actionQueue != null && _actionQueue.Count == 0)
                {
                    if (_currentGoal.Remove)
                        Goals.Remove(_currentGoal);

                    _planner = null;
                }

                if (_actionQueue != null && _actionQueue.Count > 0)
                {
                    _currentAction = _actionQueue.Dequeue();
                    if (!_currentAction.PrePerform())
                        _actionQueue = null;

                    _currentAction.Running = true;
                }
                _actionQueue = null;
                _planner = null;
            }
        }

        private void CompleteAction()
        {
            _currentAction.Running = false;
            _currentAction.PostPerform();
            _actionInvoked = false;
        }

        private Action PlayNextPosibleCard(WorldStates states)
        {
            Action firstPosibleAction = null;
            foreach (var item in Actions)
            {
                foreach (var state in states.GetStates())
                {
                    if (!item.PreConditions.ContainsKey(state.Key))
                        continue;

                    firstPosibleAction = item;
                    break;
                }

                if (firstPosibleAction != null)
                    break;
            }

            return firstPosibleAction;
        }
    }
}
