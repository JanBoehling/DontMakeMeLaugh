using GOAP;
using System.Collections.Generic;
using UnityEngine;

public class TheKingsBehaviour : Agent
{
    [SerializeField]
    private TheKingsData _data;

    public void SetAgentData(List<CardSO> cards)
    {
        ResetGameData();
        _data.AICards = cards;

        AgentBeliefs = new WorldStates();
        foreach (var item in _data.AICards)
        {
            if (item.name.Contains("King"))
                AgentBeliefs.ModifyState("hasKingCards", 1);

            if (item.name.Contains("Queen"))
                AgentBeliefs.ModifyState("hasQueenCards", 1);

            if (item.name.Contains("Peasent"))
                AgentBeliefs.ModifyState("hasPeasentCards", 1);
        }

        SubGoal winGoal = new("win", 4, false);
        Goals.Add(winGoal, 4);
        SubGoal gainPointGoal = new("gainPoint", 3, false);
        Goals.Add(gainPointGoal, 3);
        SubGoal drawGoal = new("draw", 2, false);
        Goals.Add(drawGoal, 2);
        SubGoal lostLeastGoal = new("lostLeast", 1, false);
        Goals.Add(lostLeastGoal, 1);
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

    public void ResetGameData()
    {
        _data.AICards.Clear();
        _data.AIPoints = 0;
        _data.PlayerCards.Clear();
        _data.PlayerPoints = 0;
    }
}
