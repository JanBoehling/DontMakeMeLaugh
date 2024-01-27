using BehaviorTree;
using UnityEngine;

internal class CheckCardValueCount : Node
{
    private TwentyOneData _gameData;
    private int _cardValue;
    private int _cardCount;
    private bool _valuesGreaterThen;

    public CheckCardValueCount(TwentyOneData gameData, int cardValue, int cardCount, bool valuesGreaterThen = false)
        : base()
    {
        _gameData = gameData;
        _cardValue = cardValue;
        _cardCount = cardCount;
        _valuesGreaterThen = valuesGreaterThen;
    }

    public override NodeState Evaluate()
    {
        if (_cardCount == 0 & _gameData.AITotalCardValue == 21)
            // Show directly
            return NodeState.Success;

        if (_valuesGreaterThen)
        {
            if (_gameData.AITotalCardValue < _cardValue && _gameData.AICardCount <= _cardCount)
                return NodeState.Success;
            else
                return NodeState.Failed;
        }
        else
        {
            if (_gameData.AITotalCardValue > _cardValue && _gameData.AICardCount > _cardCount)
                return NodeState.Success;
            else
                return NodeState.Failed;
        }
    }
}