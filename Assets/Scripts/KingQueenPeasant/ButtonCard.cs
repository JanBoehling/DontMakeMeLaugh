using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCard : MonoBehaviour
{
    public int WorthOfTheCard;
    private int AICard;
    private int playerWinCount;
    private int AIWinCount;
    //joker = 0;
    //queen = 1;
    //king = 2;

    public void CardWasPressed(int value)
    {
        Debug.Log(value);
        WhichWasHigher(value);
        gameObject.SetActive(false);
    }

    private void WhichWasHigher(int playerCard)
    {
        if (playerCard > AICard)
        {
            //player Wins
            playerWinCount++;
        }
        if (playerCard < AICard)
        {
            //AI Wins
            AIWinCount++;
        }
        if (playerCard == AICard)
        {
            //nothing happens
        }

        if (playerWinCount >= 3)
        {
            //player Win
        }
        else if(AIWinCount >= 3)
        {
            //AI Wins
        }
    }
}
