using NUnit.Framework;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField]
    private RPSCard dummyCard;

    public GameObject PlayersCardChoice;
    public GameObject OpponentCardChoice;

    public RPSCardTypes PlayersChoice;
    public RPSCardTypes OpponentChoice;

    private bool GameHasStarted = true;
    private bool turnInProgress = false;

    public static RockPaperScissor instance;
    public bool NextGame;

    public void OnAwake()
    {
        if (GameHasStarted)
        {
            HandHandsHand();
            if (data.playerPoints >= pointsNeeded || data.opponentPoints >= pointsNeeded)
            {
                ResolveGame();
            }
            else if (!turnInProgress)
            {
                TurnStart();
            }
        }
    }

    private void TurnStart()
    {
        turnInProgress = true;
        OpponentCardChoice = CreateCard((RPSCardTypes)UnityEngine.Random.Range(0, 2), CardPosOpponent.transform);
    }

    private void OnInteracted(Transform transform)
    {
        if (transform.position == CardPos1.transform.position)
        {
            PlayersCardChoice = CardPos1;
            PlayersChoice = RPSCardTypes.Rock;
        }
        else if (transform.position == CardPos2.transform.position)
        {
            PlayersCardChoice = CardPos2;
            PlayersChoice = RPSCardTypes.Paper;
        }

        else if (transform.position == CardPos3.transform.position)
        {
            PlayersCardChoice = CardPos3;
            PlayersChoice = RPSCardTypes.Scissors;
        }

        TurnResolve(PlayersChoice, OpponentChoice);
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
        card.transform.position = position.position;
        card.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);

        RPSCard rpsCard = card.AddComponent<RPSCard>();
        rpsCard.cardType = type;
        rpsCard.OnInteracted.AddListener(() => OnInteracted(transform));
        rpsCard.paperPrint = dummyCard.paperPrint;
        rpsCard.rockPrint = dummyCard.rockPrint;
        rpsCard.scissorPrint = dummyCard.scissorPrint;
        rpsCard.printQuad = dummyCard.transform.GetChild(0).gameObject;
        //rpsCard.ChangeCardType();
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
        NextGame = true;
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
