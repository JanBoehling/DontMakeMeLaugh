using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/TheKingsData")]
public class TheKingsData : ScriptableObject
{
    public TheKingsParticipant Owner;
    public List<ButtonCard> AICards = new List<ButtonCard>();
    public List<ButtonCard> PlayerCards = new List<ButtonCard>();
    public int AIPoints;
    public int PlayerPoints;
}
