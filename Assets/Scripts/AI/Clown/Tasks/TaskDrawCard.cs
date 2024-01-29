using BehaviorTree;

public class TaskDrawCard : Node
{
    private TwentyOneData _data;

    public TaskDrawCard(TwentyOneData data)
    {
        _data = data;
    }

    public override NodeState Evaluate()
    {
        if (_data.Game.GameEnded) return NodeState.Failed;
        int card = _data.Game.DrawCardEnemy();
        _data.AILastCardValue = card;
        _data.AITotalCardValue += card;
        _data.AICardCount++;
        return NodeState.Success;
    }
}