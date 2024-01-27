using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Card", order = 0)]
public class CardSO : ScriptableObject
{
    public int Rank;
    public Texture CardTex;
}
