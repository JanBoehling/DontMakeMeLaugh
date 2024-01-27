using System.Collections.Generic;
using UnityEngine;

public class ButtonCard : MonoBehaviour
{
    public CardSO CardSO;
    public TheKingsGame Game;

    /// <summary>
    /// Call if Card was hit by Raycast
    /// </summary>
    public void CardWasPressed()
    {
        List<CardSO> playerCards = Game.GetPlayerCards();
        for (int i = 0; i < playerCards.Count; i++)
            if (playerCards[i] == CardSO)
                Game.PlayCard(i, true);
    }
}
