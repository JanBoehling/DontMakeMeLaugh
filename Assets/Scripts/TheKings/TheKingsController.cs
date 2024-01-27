
public static class TheKingsController
{
    public static CardSO PlayedCardPlayer { get; private set; } = null;
    public static CardSO PlayedCardEnemy { get; private set; } = null;

    public static int PlayerScore { get; private set; } = 0;
    public static int EnemyScore { get; private set; } = 0;

    public static TheKingsBehaviour EnemyAgent;

    public static void PlayCard(CardSO card, TheKingsParticipant owner)
    {
        switch (owner)
        {
            case TheKingsParticipant.Player:
                PlayedCardPlayer = card;
                EnemyAgent.SetPlayerCard(card);
                break;

            case TheKingsParticipant.Enemy:
                PlayedCardEnemy = card;
                break;
        }
    }

    public static TheKingsParticipant? CompareCards()
    {
        if (PlayedCardPlayer.Rank > PlayedCardEnemy.Rank) return TheKingsParticipant.Player;
        else if (PlayedCardPlayer.Rank < PlayedCardEnemy.Rank) return TheKingsParticipant.Enemy;
        else return null;
    }

    public static void RaiseScore(TheKingsParticipant owner)
    {
        if (owner == TheKingsParticipant.Player) PlayerScore++;
        else EnemyScore++;

        if (PlayerScore >= 3) Winner(TheKingsParticipant.Player);
        else if (EnemyScore >= 3)
        {
            EnemyAgent.AgentBeliefs.ModifyState("hasPoint", 1);
            Winner(TheKingsParticipant.Enemy);
        }
    }

    public static void Winner(TheKingsParticipant winner)
    {
        // Callback end game
    }
}