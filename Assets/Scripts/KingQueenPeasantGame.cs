using System.Collections.Generic;
using UnityEngine;

public static class KingQueenPeasantGameController
{
    public static CardSO PlayedCardPlayer { get; private set; } = null;
    public static CardSO PlayedCardEnemy { get; private set; } = null;

    public static int PlayerScore { get; private set; } = 0;
    public static int EnemyScore { get; private set; } = 0;

    public static void PlayCard(CardSO card, KingQueenPeasantParticipant owner)
    {
        switch (owner)
        {
            case KingQueenPeasantParticipant.Player:
                PlayedCardPlayer = card;
                break;

            case KingQueenPeasantParticipant.Enemy:
                PlayedCardEnemy = card;
                break;
        }
    }

    public static KingQueenPeasantParticipant? CompareCards()
    {
        if (PlayedCardPlayer.Rank > PlayedCardEnemy.Rank) return KingQueenPeasantParticipant.Player;
        else if (PlayedCardPlayer.Rank < PlayedCardEnemy.Rank) return KingQueenPeasantParticipant.Enemy;
        else return null;
    }

    public static void RaiseScore(KingQueenPeasantParticipant owner)
    {
        if (owner == KingQueenPeasantParticipant.Player) PlayerScore++;
        else EnemyScore++;

        if (PlayerScore >= 3) Winner(KingQueenPeasantParticipant.Player);
        else if (EnemyScore >= 3) Winner(KingQueenPeasantParticipant.Enemy);
    }

    public static void Winner(KingQueenPeasantParticipant winner)
    {
        // Callback end game
    }
}

public class KingQueenPeasantGame : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab = null;
    [SerializeField] private Material cardFrontMaterial = null;

    [SerializeField] private CardSO[] possibleCards = new CardSO[3];

    private readonly List<CardSO> cards = new(5);

    private readonly List<GameObject> cardObjects = new(5);

    // Phase 1: every participant gets five cards
    public void DealCards()
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
        var playedCard = cards[index];
        cards.RemoveAt(index);

        KingQueenPeasantGameController.PlayCard(playedCard, KingQueenPeasantParticipant.Player);
    }

    // Phase 3: both played cards ranks are compared. the one with the higher rank gets a point
    public void CompareCards()
    {
        var winner = KingQueenPeasantGameController.CompareCards();

        if (winner is not null) KingQueenPeasantGameController.RaiseScore((KingQueenPeasantParticipant)winner);
        else return;
    }
}
