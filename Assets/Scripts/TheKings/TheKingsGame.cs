using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheKingsGame : MonoBehaviour
{
    [SerializeField] private GameObject[] cardPrefab = new GameObject[3];
    [SerializeField] private Material cardFrontMaterial = null;
    [SerializeField] private TheKingsBehaviour enemy;
    [SerializeField] private Transform cardSpawnPoint;
    [SerializeField] private LayerMask playerCardLayer;

    [SerializeField] private CardSO[] possibleCards = new CardSO[3];

    private List<CardSO> playerCards = new(5);
    private List<CardSO> aICards = new(5);

    private List<GameObject> playerCardObjects = new(5);
    private List<GameObject> AICardObjects = new(5);
    private bool playerPlayed;
    private bool enemyPlayed;
    private bool gameStarted = false;

    public bool GameFinished;
    public UnityEvent<string, GameObject> PlayAudioEvent;

    private void Start()
    {
        TheKingsController.EnemyAgent = enemy;
        StartGame();
    }

    public void StartGame()
    {
        if (!gameStarted) 
        {
            if (TheKingsController.EnemyAgent == null)
                TheKingsController.EnemyAgent = enemy;
            DealCards(playerCards, playerCardObjects);
            DealCards(aICards, AICardObjects, true);

            enemy.GetGameData().AICards = aICards;
            gameStarted = true;
        }
    }

    // Phase 1: every participant gets five cards
    public void DealCards(List<CardSO> cards, List<GameObject> cardObjects, bool isEnemy = false)
    {
        cards.Clear();

        PlayAudioEvent?.Invoke("ChildLabour", enemy.gameObject);

        const int cardAmount = 5;
        float offset = 0;
        for (int i = 0; i < cardAmount; i++)
        {
            var randomCardIndex = UnityEngine.Random.Range(0, possibleCards.Length);
            var card = possibleCards[randomCardIndex];
            cards.Add(card);

            GameObject obj = Instantiate(cardPrefab[randomCardIndex]);
            if (isEnemy)
            {
                obj.transform.localPosition = new Vector3(cardSpawnPoint.position.x + offset, cardSpawnPoint.position.y + 0.2f, cardSpawnPoint.position.z + 0.5f);
                obj.transform.eulerAngles = new Vector3(90, 0, 0);
            }
            else
            {
                obj.transform.localPosition = new Vector3(cardSpawnPoint.position.x + offset, cardSpawnPoint.position.y + 0.2f, cardSpawnPoint.position.z);
                obj.transform.eulerAngles = new Vector3(90, 180, 30);
                obj.layer = 18;
            }
            obj.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
            offset += 0.2f;
            if (cardObjects.Count < 5)
            {
                cardObjects.Add(obj);
            }
            else
            {
                cardObjects[i] = obj;
            }
            ButtonCard button = cardObjects[i].AddComponent<ButtonCard>();
            button.CardSO = card;
            button.Game = this;
            button.Index = i;
        }

        if (isEnemy)
            enemy.SetAgentData(cards);
    }

    // Phase 2: every participant plays one card covered
    public void PlayCard(int index, bool isPlayer = false)
    {
        CardSO playedCard;
        if (isPlayer)
        {
            playedCard = playerCards[index];
            playerCards[index] = null;
            Destroy(playerCardObjects[index]);
            playerCardObjects[index] = null;
            TheKingsController.PlayCard(playedCard, TheKingsParticipant.Player);
            if (playedCard.Rank == 0) PlayAudioEvent?.Invoke("NoBetterThanPeasent", enemy.gameObject);
            playerPlayed = true;
        }
        else if (!enemyPlayed)
        {
            playedCard = aICards[index];
            aICards[index] = null;
            Destroy(AICardObjects[index]);
            AICardObjects[index] = null;
            TheKingsController.PlayCard(playedCard, TheKingsParticipant.Enemy);
            enemyPlayed = true;
        }

        if (playerPlayed && enemyPlayed)
            CompareCards();
    }

    // Phase 3: both played cards ranks are compared. the one with the higher rank gets a point
    public void CompareCards()
    {
        var winner = TheKingsController.CompareCards();
        bool hasWinner = false;

        if (winner is not null)
            hasWinner = TheKingsController.RaiseScore((TheKingsParticipant)winner);

        playerPlayed = false;
        enemyPlayed = false;

        if (!hasWinner)
            return;
        else
            GameFinished = true;

        foreach (var item in playerCardObjects)
        {
            Destroy(item);
        }
        foreach (var item in AICardObjects)
        {
            Destroy(item);
        }

        playerCards.Clear();
        playerCardObjects.Clear();
        aICards.Clear();
        AICardObjects.Clear();

        Console.WriteLine("Game Finished! Do Killanimation!");
        Console.WriteLine("You are Dead!!!");

        // Voiceline lost all games
        if (UnityEngine.Random.Range(0, 2) == 0)
            PlayAudioEvent?.Invoke("BetterLuckNextTime", enemy.gameObject);
        else
            PlayAudioEvent?.Invoke("BiggerPlansForFingers", enemy.gameObject);
    }

    public List<CardSO> GetPlayerCards() => playerCards;

    private IEnumerator AsyncWaitingForPlayer(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (seconds <= 10)
            PlayAudioEvent?.Invoke("FinallyPlayCards", enemy.gameObject);
        else if (seconds <= 20)
            PlayAudioEvent?.Invoke("InfiniteTimePerformance", enemy.gameObject);
        else if (seconds <= 30)
            PlayAudioEvent?.Invoke("COMEON", enemy.gameObject);
        else if (seconds <= 40)
            PlayAudioEvent?.Invoke("PLAYYOURFUCKMOVE", enemy.gameObject);
    }
}
