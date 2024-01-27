using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/TheKingsData")]
public class TheKingsData : ScriptableObject
{
    public TheKingsParticipant Owner;
    public List<CardSO> AICards = new List<CardSO>();
    public List<CardSO> PlayerCards = new List<CardSO>();
    public int AIPoints;
    public int PlayerPoints;
}
