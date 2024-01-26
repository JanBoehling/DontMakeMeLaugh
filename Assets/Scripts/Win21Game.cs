using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//[CreateAssetMenu(menuName = "MiniGames/Win21")]

public class Win21Game : MonoBehaviour
{
    private int minRandom = 1;
    private int maxRandom = 11;
    private int maxPoints = 21;
    private int currentPoints;
    private int currentPointsAI;
    public int NumberOnCard;
    private bool alreadyLost;


    private void Update()
    {
        GameStillPlayable();
    }

    private void GameStillPlayable()
    {
        if (alreadyLost == false)
        {
            if (currentPoints < maxPoints)
                AnotherCard();
            else
                alreadyLost = true;
        }
        else
            GameEnd();
    }

    private void AnotherCard()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int random = UnityEngine.Random.Range(minRandom, maxRandom);
            NumberOnCard = random;
            currentPoints += random;
            Debug.Log(currentPoints);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
            return;
    }

    private void GameEnd()
    {
        if (currentPoints <= maxPoints && currentPoints > currentPointsAI && currentPointsAI <= maxPoints)
        {
            //Player Win, because he is closer to 21 than the AI.
        }
        else if(currentPointsAI <= maxPoints && currentPointsAI > currentPoints && currentPoints <= maxPoints)
        {
            //Player Lose, because ai is closer to 21.
        }
        if (currentPoints > maxPoints && currentPointsAI <= maxPoints)
        {
            //player lose, because player is over 21 while AI is under it.
        }
        else if (currentPointsAI > maxPoints && currentPoints <= maxPoints)
        {
            //Player win, because ai is over 21 while player is not.
        }
        if (currentPoints > maxPoints && currentPointsAI > maxPoints && currentPoints > currentPointsAI)
        {
            //player lose, because both people are over the Limit but Player has a higher number
        }
        else if(currentPointsAI > maxPoints && currentPoints > maxPoints && currentPointsAI > currentPoints)
        {
            //Player Win, because both are over 21 but he has the lower number
        }
        if (currentPoints == currentPointsAI)
        {
            //Noone Wins
        }
    }
}
