using BehaviorTree;

public class TaskOverTwentyOne : Node
{
    private TwentyOneData _data;

    public TaskOverTwentyOne(TwentyOneData data)
    {
        _data = data;
    }

    public override NodeState Evaluate()
    {
        if (!_data.Game.CheckStillPlayable(_data.AITotalCardValue))
        {
            _data.Game.OnAIOverFitted();
            return base.Evaluate();
        }
        return NodeState.Success;
    }
}