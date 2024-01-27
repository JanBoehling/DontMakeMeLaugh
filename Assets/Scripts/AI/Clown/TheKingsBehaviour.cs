using GOAP;
using UnityEngine;

public class TheKingsBehaviour : Agent
{
    //[SerializeField]
    //private TheKingsData

    protected override void Start()
    {
        AgentBeliefs = new WorldStates();
        //AgentBeliefs.ModifyState();

        SubGoal goal = new("win", 1, false);
        Goals.Add(goal, 1);
        base.Start();
    }

    public void SetPlayerCard(CardSO card)
    {
        switch(card.Rank)
        {
            case 0: // Jokl
                AgentBeliefs.ModifyState("playerPlayedPeasent", 1);
                break;
            case 1: // Queen
                AgentBeliefs.ModifyState("playerPlayedQueen", 1);
                break;
            case 2: // King
                AgentBeliefs.ModifyState("playerPlayedKing", 1);
                break;
        }
    }

    public TheKingsData GetGameData()
    {
        return _data;
    }
}
