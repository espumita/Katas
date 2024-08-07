namespace Bowling;

public class BowlingGame{
    public readonly Dictionary<Player, FrameScores> Scores;

    public BowlingGame(IEnumerable<Player> players) {
        Scores = new Dictionary<Player, FrameScores>();
        foreach (var player in players) {
            Scores.Add(player, new FrameScores());
        }
    }

    public void Roll(Player player, int knockDownPins) {
        Scores[player].Roll(knockDownPins);
    }

    public bool GameEnds() {
        var lastPlayer = Scores.Keys.ToList().Last();
        return Scores[lastPlayer].PlayerEnds();
    }
}