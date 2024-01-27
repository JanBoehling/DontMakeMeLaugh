using GOAP;
using UnityEngine;

public class ActionPlayQueen : Action
{
    [SerializeField]
    private TheKingsGame _game;
    [SerializeField]
    private TheKingsBehaviour _agent;

    public override bool PostPerform()
    {
        _agent.AgentBeliefs.ModifyState("hasQueenCards", -1);

        return true;
    }

    public override bool PrePerform()
    {
        for (int i = 0; i < _agent.GetGameData().AICards.Count; i++)
            if (_agent.GetGameData().AICards[i].Rank == 1)
            {
                _game.PlayCard(i);
                return true;
            }

        return false;
    }
}
