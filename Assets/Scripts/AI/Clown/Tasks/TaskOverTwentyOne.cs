using ProjectBarde.BehaviorTree;

public class TaskOverTwentyOne : Node
{
    private TwentyOneData _data;

    public TaskOverTwentyOne(TwentyOneData data)
    {
        _data = data;
    }

    public override NodeState Evaluate()
    {
        if (_data.Game.CheckStillPlayable(_data.AITotalCardValue))
        {
            _data.Game.OnAIOverFitted();
            return NodeState.Success;
        }
        return base.Evaluate();
    }
}