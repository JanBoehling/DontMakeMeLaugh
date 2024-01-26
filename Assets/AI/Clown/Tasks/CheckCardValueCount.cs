using ProjectBarde.BehaviorTree;
using UnityEngine;

internal class CheckCardValueCount : Node
{
    private GameObject _gameData;
    private int _cardValue;
    private int _cardCount;
    private bool _valuesGreaterThen;

    public CheckCardValueCount(GameObject gameData, int cardValue, int cardCount, bool valuesGreaterThen = false)
        : base()
    {
        _gameData = gameData;
        _cardValue = cardValue;
        _cardCount = cardCount;
        _valuesGreaterThen = valuesGreaterThen;
    }

    public override NodeState Evaluate()
    {
        if (_cardCount == 0 & _cardValue == 21)
            // pass
            return NodeState.Success;

        if (_valuesGreaterThen)
        {
            if (_gameData.GesCardValue < _cardValue && _gameData.CardCount <= _cardCount)
                return NodeState.Success;
            else
                return NodeState.Failed;
        }
        else
        {
            if (_gameData.GesCardValue > _cardValue && _gameData.CardCount > _cardCount)
                return NodeState.Success;
            else
                return NodeState.Failed;
        }
    }
}