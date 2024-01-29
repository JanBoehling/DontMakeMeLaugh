using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Win21Game : MonoBehaviour
{
    [SerializeField]
    private TwentyOneData data;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject card;
    [SerializeField]
    private Transform cardSpawnPoint;
    [SerializeField]
    NumberContainerScriptx numberContainer;
    [SerializeField]
    private Material cardMaterial = null;

    public UnityEvent<string, GameObject> AudioPlayEvent;

    private List<GameObject> garbageContainer = new List<GameObject>();
    private int minRandom = 1;
    private int maxRandom = 11;
    private int maxPoints = 21;
    private float cardOffsetPlayer;
    private float cardOffsetAI;
    public int NumberOnCard;
    private bool playerAlreadyLost;
    private bool aiAlreadyLost;
    private bool playersTurn = true;
    public bool GameEnded;
    public bool NextGame;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform finger;
    [SerializeField] private Transform cardHoldingPoint;
    private GameObject cardOnTable;
    [SerializeField] private GameObject cardImageContainerPlayer;
    [SerializeField] private GameObject cardImageContainerEnemy;
    [SerializeField] private GameObject cardImagePrefab;
    [SerializeField] private DrawSequence enemyDrawSequence;
    private const int roundCount = 3;

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
        // Game ended: press space to init next game
        if (GameEnded && Input.GetKeyDown(KeyCode.Space))
        {
            NextGame = true;
            foreach (var item in garbageContainer)
            {
                Destroy(item);
            }
            garbageContainer.Clear();
            return;
        }
        else if (GameEnded)
            return;

        // one particpant lost: end game
        if (playerAlreadyLost || aiAlreadyLost)
        {
            GameEnd();
            return;
        }

        // Taking turns
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

    public int DrawCardPlayer()
    {
        int random = Random.Range(minRandom, maxRandom);
        NumberOnCard = random;
        StartCoroutine(InstantiateCardCO());
        return random;
    }

    public int DrawCardEnemy()
    {
        int random = Random.Range(minRandom, maxRandom);
        NumberOnCard = random;
        enemyDrawSequence.PlaySequence();

        // Display card in ui
        var cardUI = Instantiate(cardImagePrefab, cardImageContainerEnemy.transform);
        cardUI.transform.GetChild(0).GetComponent<Image>().sprite = numberContainer.GetCardUI(NumberOnCard);
        return random;
    }

    /// <summary>
    /// Checks if player is still under 21 points
    /// </summary>
    private void GameStillPlayable()
    {
        if (data.PlayerTotalCardValue < maxPoints)
            GetCardDrawInput();
        else
            playerAlreadyLost = true;
    }

    private void GetCardDrawInput()
    {
        if (playersTurn && Input.GetMouseButtonDown(0))
        {
            data.PlayerTotalCardValue += DrawCardPlayer();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            playersTurn = false;
        }
    }

    private IEnumerator InstantiateCardCO()
    {
        // Play anim
        playerAnimator.SetTrigger("DoCard");

        // After x seconds, spawn card
        yield return new WaitForSeconds(1f);

        // Destroy old card
        if (cardOnTable) cardOnTable.SetActive(false);
        if (cardOnTable) garbageContainer.Add(cardOnTable);

        // Instantiat new card
        cardOnTable = numberContainer.GetCardPrefab(NumberOnCard);
        cardOnTable.transform.position = finger.position;
        cardOnTable.transform.SetParent(finger);
        cardOnTable.transform.localScale *= .25f; 
        var rotator = cardOnTable.AddComponent<CardRotator>();
        rotator.InAnimation = true;

        // After x more seconds, unparent card
        yield return new WaitForSeconds(1f);
        cardOnTable.transform.SetParent(null, true);

        // Display card in ui
        var cardUI = Instantiate(cardImagePrefab, cardImageContainerPlayer.transform);
        cardUI.transform.GetChild(0).GetComponent<Image>().sprite = numberContainer.GetCardUI(NumberOnCard);

        // Lerp card to card holding point  
        //float duration = 1.5f;
        //float elapsedTime = 0;
        //while (elapsedTime < duration)
        //{
        //    itemObject.transform.position = Vector3.Lerp(itemObject.transform.position, cardHoldingPoint.position, elapsedTime / duration);
        //
        //    elapsedTime += Time.deltaTime;
        //
        //    yield return null;
        //}

        rotator.InAnimation = false;
    }

    //private void InstantiateCard()
    //{
    //    var cardPrefab = numberContainer.GetCardPrefab(NumberOnCard);
    //    var itemObject = Instantiate(cardPrefab, cardSpawnPoint);

    //    if (playersTurn == true)
    //    {
    //        itemObject.transform.localPosition = new Vector3(cardOffsetPlayer, 0, 0);
    //        itemObject.transform.eulerAngles = new Vector3(0, 180, 0);
    //        itemObject.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
    //        cardOffsetPlayer += 0.3f;
    //    }
    //    else
    //    {
    //        itemObject.transform.localPosition = new Vector3(cardOffsetAI, 0, 0.5f);
    //        itemObject.transform.eulerAngles = Vector3.zero;
    //        itemObject.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
    //        cardOffsetAI += 0.3f;
    //    }

    //    garbageContainer.Add(itemObject);
    //}

    private void GameEnd()
    {
        if (data.PlayerTotalCardValue <= maxPoints && data.PlayerTotalCardValue > data.AITotalCardValue && data.AITotalCardValue <= maxPoints)
        {
            //Player Win, because he is closer to 21 than the AI.
            AudioPlayEvent?.Invoke("FINGIES", enemy);
            RoundCountHolder.PlayerWon++;
        }
        else if(data.AITotalCardValue <= maxPoints && data.AITotalCardValue > data.PlayerTotalCardValue && data.PlayerTotalCardValue <= maxPoints)
        {
            //Player Lose, because ai is closer to 21.
            AudioPlayEvent?.Invoke("BrightSmileHug", enemy);
        }
        if (data.PlayerTotalCardValue > maxPoints && data.AITotalCardValue <= maxPoints)
        {
            //player lose, because player is over 21 while AI is under it.
            AudioPlayEvent?.Invoke("EasierThanEgg", enemy);
        }
        else if (data.AITotalCardValue > maxPoints && data.PlayerTotalCardValue <= maxPoints)
        {
            //Player win, because ai is over 21 while player is not.
            AudioPlayEvent?.Invoke("21Thoughts", enemy);
            RoundCountHolder.PlayerWon++;
        }
        if (data.PlayerTotalCardValue > maxPoints && data.AITotalCardValue > maxPoints && data.PlayerTotalCardValue > data.AITotalCardValue)
        {
            //player lose, because both people are over the Limit but Player has a higher number
            AudioPlayEvent?.Invoke("ExcitmentThroughMyBody", enemy);
        }
        else if(data.AITotalCardValue > maxPoints && data.PlayerTotalCardValue > maxPoints && data.AITotalCardValue > data.PlayerTotalCardValue)
        {
            //Player Win, because both are over 21 but he has the lower number
            AudioPlayEvent?.Invoke("DropKickAChild", enemy);
            RoundCountHolder.PlayerWon++;
        }
        if (data.PlayerTotalCardValue == data.AITotalCardValue)
        {
            //Noone Wins
            AudioPlayEvent?.Invoke("ComingForFingers", enemy);
        }

        RoundCountHolder.RoundCount++;

        GameEnded = true;
    }
}
