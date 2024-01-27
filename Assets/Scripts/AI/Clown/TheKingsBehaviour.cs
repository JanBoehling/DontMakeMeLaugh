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
}
