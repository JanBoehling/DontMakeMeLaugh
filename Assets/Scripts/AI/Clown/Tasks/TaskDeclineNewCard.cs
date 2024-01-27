using BehaviorTree;

public class TaskDeclineNewCard : Node
{
    private TwentyOneData _data;
    public TaskDeclineNewCard(TwentyOneData data)
    {
        _data = data;
    }

    public override NodeState Evaluate()
    {
        _data.Game.OnAIFinishedDrawing();
        return base.Evaluate();
    }
}