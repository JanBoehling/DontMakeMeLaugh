using ProjectBarde.BehaviorTree;

public class TaskDrawCard : Node
{
    private TwentyOneData _data;

    public TaskDrawCard(TwentyOneData data)
    {
        _data = data;
    }

    public override NodeState Evaluate()
    {
        int card = _data.Game.DrawCard();
        _data.AILastCardValue = card;
        _data.AITotalCardValue += card;
        _data.AICardCount++;
        return NodeState.Success;
    }
}