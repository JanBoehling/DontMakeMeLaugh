using ProjectBarde.BehaviorTree;
using UnityEngine;

public class TaskDrawSymbol : Node
{
    private int _states;

    public TaskDrawSymbol(int states) 
        : base()
    {
        _states = states;
    }

    public override NodeState Evaluate()
    {
        int rand = Random.Range(0, _states);
        // TODO: Send rand to needed Place

        return base.Evaluate();
    }
}
