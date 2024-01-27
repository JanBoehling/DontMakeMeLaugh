using System.Collections.Generic;
using UnityEngine;

public class TheKingsGame : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab = null;
    [SerializeField] private Material cardFrontMaterial = null;
    [SerializeField] private TheKingsBehaviour enemy;

    [SerializeField] private CardSO[] possibleCards = new CardSO[3];

    private readonly List<CardSO> playerCards = new(5);
    private readonly List<CardSO> AICards = new(5);

    private readonly List<GameObject> playerCardObjects = new(5);
    private readonly List<GameObject> AICardObjects = new(5);

    private void Start()
    {
        TheKingsController.EnemyAgent = enemy;
        DealCards(playerCards, playerCardObjects);
        DealCards(AICards, AICardObjects);

        enemy.GetGameData().AICards = AICards;
    }

    // Phase 1: every participant gets five cards
    public void DealCards(List<CardSO> cards, List<GameObject> cardObjects)
    {
        cards.Clear();

        const int cardAmount = 5;
        for (int i = 0; i < cardAmount; i++)
        {
            var randomCardIndex = Random.Range(0, possibleCards.Length);
            var card = possibleCards[randomCardIndex];
            cards.Add(card);

            cardObjects[i] = Instantiate(cardPrefab);
            cardFrontMaterial.SetTexture("BaseColorMap", card.CardTex);
        }
    }

    // Phase 2: every participant plays one card covered
    public void PlayCard(int index)
    {
        var playedCard = AICards[index];
        AICards.RemoveAt(index);

        TheKingsController.PlayCard(playedCard, TheKingsParticipant.Player);
    }

    // Phase 3: both played cards ranks are compared. the one with the higher rank gets a point
    public void CompareCards()
    {
        var winner = TheKingsController.CompareCards();

        if (winner is not null) TheKingsController.RaiseScore((TheKingsParticipant)winner);
        else return;
    }
}
