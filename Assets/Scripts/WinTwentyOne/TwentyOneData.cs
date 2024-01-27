using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/TwentyOneData")]
public class TwentyOneData : ScriptableObject
{
    public Win21Game Game;
    public int AITotalCardValue;
    public int AICardCount;
    public int AILastCardValue;
    public int PlayerTotalCardValue;
    public int PlayerCardCount;
    public int PlayerLastCardValue;
}
