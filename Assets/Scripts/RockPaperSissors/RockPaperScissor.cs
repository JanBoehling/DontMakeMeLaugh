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

    private bool playerPlayedCard = false;


    private void Start()
    {
        
    }

    private void Update()
    {
        if (data.playerPoints>=pointsNeeded || data.opponentPoints >= pointsNeeded)
        {
            ResolveGame();
        }
        else if (playerPlayedCard)
        {
            
        }
    }

    private void HandHandHands()
    {
        CreateCard(RPSCardTypes.Rock);
        CreateCard(RPSCardTypes.Paper);
        CreateCard(RPSCardTypes.Scissors);
    }

    private void CreateCard(RPSCardTypes type)
    {
        Card = Instantiate(Card);
        Card.GetComponent<RPSCard>().cardType = type;
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
