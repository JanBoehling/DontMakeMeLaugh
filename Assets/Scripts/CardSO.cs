using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Card", order = 0)]
public class CardSO : ScriptableObject
{
    public TheKingsParticipant Owner;
    public int Rank;
    public Texture CardTex;
}
