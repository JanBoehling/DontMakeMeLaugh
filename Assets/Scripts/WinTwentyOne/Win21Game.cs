using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class Win21Game : MonoBehaviour
{
    [SerializeField]
    private TwentyOneData data;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject card;
    [SerializeField]
    private Camera playerCam;
    [SerializeField]
    NumberContainerScriptx numberContainer;
    [SerializeField]
    private Material cardMaterial = null;


    private int minRandom = 1;
    private int maxRandom = 11;
    private int maxPoints = 21;
    private float cardShiftPlayer;
    private float cardShiftAI;
    public int NumberOnCard;
    private bool playerAlreadyLost;
    private bool aiAlreadyLost;
    private bool playersTurn = true;
    private bool gameEnded;

    private void Start()
    {
        data.AICardCount = 0;
        data.AILastCardValue = 0;
        data.AITotalCardValue = 0;
        data.PlayerTotalCardValue = 0;
        data.Game = this;   
    }

    private void Update()
    {
        if (gameEnded) return;

        if (playerAlreadyLost || aiAlreadyLost)
            GameEnd();

        if (playersTurn)
        {
            Debug.Log("Drawing a Card");
            GameStillPlayable();
        }
        else // AI turn
        {
            enemy.GetComponent<TwentyOneBehaviour>().UpdateTree();
        }
    }

    public bool CheckStillPlayable(int currentPoints)
    {
        if (currentPoints > maxPoints)
            return false;
        return true;
    }

    public void OnAIOverFitted()
    {
        aiAlreadyLost = true;
    }

    public void OnAIFinishedDrawing()
    {
        GameEnd();
    }

    public int DrawCard()
    {
        int random = Random.Range(minRandom, maxRandom);
        NumberOnCard = random;
        Debug.Log(NumberOnCard);
        ProduceCard();
        return random;
    }

    private void GameStillPlayable()
    {
        if (data.PlayerTotalCardValue < maxPoints)
            AnotherCard();
        else
            playerAlreadyLost = true;
    }

    private void AnotherCard()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            data.PlayerTotalCardValue += DrawCard();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            playersTurn = false;
        }
    }

    private void ProduceCard()
    {
        numberContainer.GettingTheCorrectTexture(NumberOnCard);
        GameObject itemObject = Instantiate(numberContainer.correctGameObject, playerCam.transform);

        if (playersTurn == true)
        {
            itemObject.transform.localPosition = new Vector3(cardShiftPlayer, -0.4f, 0.5f);
            cardShiftPlayer += 0.5f;
        }
        else
        {
            itemObject.transform.localPosition = new Vector3(cardShiftAI, -0.4f, 1f);
            cardShiftAI += 0.5f;
        }

    }

    private void GameEnd()
    {
        if (data.PlayerTotalCardValue <= maxPoints && data.PlayerTotalCardValue > data.AITotalCardValue && data.AITotalCardValue <= maxPoints)
        {
            //Player Win, because he is closer to 21 than the AI.
        }
        else if(data.AITotalCardValue <= maxPoints && data.AITotalCardValue > data.PlayerTotalCardValue && data.PlayerTotalCardValue <= maxPoints)
        {
            //Player Lose, because ai is closer to 21.
        }
        if (data.PlayerTotalCardValue > maxPoints && data.AITotalCardValue <= maxPoints)
        {
            //player lose, because player is over 21 while AI is under it.
        }
        else if (data.AITotalCardValue > maxPoints && data.PlayerTotalCardValue <= maxPoints)
        {
            //Player win, because ai is over 21 while player is not.
        }
        if (data.PlayerTotalCardValue > maxPoints && data.AITotalCardValue > maxPoints && data.PlayerTotalCardValue > data.AITotalCardValue)
        {
            //player lose, because both people are over the Limit but Player has a higher number
        }
        else if(data.AITotalCardValue > maxPoints && data.PlayerTotalCardValue > maxPoints && data.AITotalCardValue > data.PlayerTotalCardValue)
        {
            //Player Win, because both are over 21 but he has the lower number
        }
        if (data.PlayerTotalCardValue == data.AITotalCardValue)
        {
            //Noone Wins
        }

        gameEnded = true;
    }
}
