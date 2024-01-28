using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheKingsGame : MonoBehaviour
{
    [SerializeField] private GameObject[] cardPrefab = new GameObject[3];
    [SerializeField] private Material cardFrontMaterial = null;
    [SerializeField] private TheKingsBehaviour enemy;

    [SerializeField] private CardSO[] possibleCards = new CardSO[3];

    private List<CardSO> playerCards = new(5);
    private List<CardSO> AICards = new(5);

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
            DealCards(AICards, AICardObjects);

            enemy.GetGameData().AICards = AICards;
            gameStarted = true;
        }
    }

    // Phase 1: every participant gets five cards
    public void DealCards(List<CardSO> cards, List<GameObject> cardObjects)
    {
        cards.Clear();

        PlayAudioEvent?.Invoke("ChildLabour", enemy.gameObject);

        const int cardAmount = 5;
        for (int i = 0; i < cardAmount; i++)
        {
            var randomCardIndex = Random.Range(0, possibleCards.Length);
            var card = possibleCards[randomCardIndex];
            cards.Add(card);

            if (cardObjects.Count < 5)
            {
                cardObjects.Add(Instantiate(cardPrefab[randomCardIndex]));
            }
            else
            {
                cardObjects[i] = Instantiate(cardPrefab[randomCardIndex]);
            }
            ButtonCard button = cardObjects[i].AddComponent<ButtonCard>();
            button.CardSO = card;
        }
    }

    // Phase 2: every participant plays one card covered
    public void PlayCard(int index, bool isPlayer = false)
    {
        CardSO playedCard;
        if (isPlayer)
        {
            playedCard = playerCards[index];
            playerCards.RemoveAt(index);
            TheKingsController.PlayCard(playedCard, TheKingsParticipant.Player);
            playerPlayed = true;
        }
        else
        {
            playedCard = AICards[index];
            AICards.RemoveAt(index);
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
            StartGame();
        else
            GameFinished = true;

        playerCards.Clear();
        playerCardObjects.Clear();
        AICards.Clear();
        AICardObjects.Clear();
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
