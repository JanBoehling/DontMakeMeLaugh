using NUnit.Framework;
using System;
using UnityEngine;

public class RockPaperScissor : MonoBehaviour
{
    [SerializeField]
    private RockPaperSissorsData data;
    [SerializeField]
    private GameObject Card;
    [SerializeField]
    private int pointsNeeded = 2;
    [SerializeField]
    private GameObject CardPos1;
    [SerializeField]
    private GameObject CardPos2;
    [SerializeField]
    private GameObject CardPos3;
    [SerializeField]
    private GameObject CardPosOpponent;

    private bool turnInProgress = false;


    private void Start()
    {
        
    }

    private void Update()
    {
        if (data.playerPoints>=pointsNeeded || data.opponentPoints >= pointsNeeded)
        {
            ResolveGame();
        }
        else if(!turnInProgress)
        {
            TurnStart();
        }
    }

    private void TurnStart()
    {
        turnInProgress = true;
        GameObject opponentsCard = CreateCard((RPSCardTypes)UnityEngine.Random.Range(0, 2), CardPosOpponent.transform);
        HandHandsHand();
    }

    private void HandHandsHand()
    {
        CreateCard(RPSCardTypes.Rock, CardPos1.transform);
        CreateCard(RPSCardTypes.Paper, CardPos2.transform);
        CreateCard(RPSCardTypes.Scissors, CardPos3.transform);
    }

    private GameObject CreateCard(RPSCardTypes type, Transform position)
    {
        GameObject card = Instantiate(Card, position);
        card.GetComponent<RPSCard>().cardType = type;
        return card;
    }

    private void ResolveGame()
    {
        if (data.playerPoints >= data.opponentPoints)
        {
            //PlayerWins();
        }
        else
        {
            //OpponentWins();
        }
        throw new NotImplementedException();
    }

    private void TurnResolve(RPSCardTypes playerCard, RPSCardTypes opponentCard)
    {
        switch(playerCard)
        {
            case RPSCardTypes.Rock:
                switch (opponentCard)
                {
                    case RPSCardTypes.Rock:
                        break;
                    case RPSCardTypes.Paper:
                        data.opponentPoints++;
                        break;
                    case RPSCardTypes.Scissors:
                        data.playerPoints++;
                        break;
                }
                //TakeBackCard();
                return;
            case RPSCardTypes.Paper:
                switch (opponentCard)
                {
                    case RPSCardTypes.Rock:
                        data.playerPoints++;
                        break;
                    case RPSCardTypes.Paper:
                        break;
                    case RPSCardTypes.Scissors:
                        data.opponentPoints++;
                        break;
                }
                //TakeBackCard();
                return;
            case RPSCardTypes.Scissors:
                switch (opponentCard)
                {
                    case RPSCardTypes.Rock:
                        data.opponentPoints++;
                        break;
                    case RPSCardTypes.Paper:
                        data.playerPoints++;
                        break;
                    case RPSCardTypes.Scissors:
                        break;
                }
                //TakeBackCard();
                return;
        }
    }
}
