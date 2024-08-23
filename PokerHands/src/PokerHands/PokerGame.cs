namespace PokerHands;

public class PokerGame{
    private List<Hand> playerHands;

    public PokerGame() {
        playerHands = new List<Hand>();
    }

    public void AddPlayerHand(string playerName, List<Card> cards) {
        playerHands.Add(new Hand(playerName, cards));
    }

    public ComparisonResult CompareHands() {
        var rankedHands = playerHands
            .Select(hand => (hand: hand, rank: hand.Rank()))
            .OrderByDescending(rank => rank.rank);
        var winner = rankedHands.First();
        return new ComparisonResult(winner.hand.playerName, winner.rank);
    }

}