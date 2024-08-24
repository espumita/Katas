namespace Tennis;


public class TennisGame6 : ITennisGame {
    private PlayerPoints6 player1Points;
    private PlayerPoints6 player2Points;

    public TennisGame6(string player1Name, string player2Name) {
        player1Points = new PlayerPoints6(player1Name);
        player2Points = new PlayerPoints6(player2Name);
    }

    public void WonPoint(string playerName) {
        if (player1Points.IsPlayer(playerName))
            player1Points.WonPoint();
        else
            player2Points.WonPoint();
    }

    public string GetScore() {
        if (BothPlayersHasSamePoints()) return player1Points.AllCall();
        return AnyPlayerHasFourOrMorePoints() 
            ? MoreThanFourPointsCall() 
            : $"{player1Points.CallForPoints()}-{player2Points.CallForPoints()}";
    }

    private bool BothPlayersHasSamePoints() {
        return player1Points.Points == player2Points.Points;
    }

    private bool AnyPlayerHasFourOrMorePoints() {
        return player1Points.Points >= 4 || player2Points.Points >= 4;
    }

    private string MoreThanFourPointsCall() {
        return (player1Points.Points - player2Points.Points) switch {
            1 => $"Advantage {player1Points.PlayerName}",
            -1 => $"Advantage {player2Points.PlayerName}",
            >= 2 => $"Win for {player1Points.PlayerName}",
            _ => $"Win for {player2Points.PlayerName}"
        };
    }
}

public class PlayerPoints6 {
    public int Points {get; private set; }
    public string PlayerName { get; private set; }

    public PlayerPoints6(string playerName) {
        this.PlayerName = playerName;
    }

    public bool IsPlayer(string playerName) {
        return PlayerName == playerName;
    }

    public void WonPoint() {
        Points++;
    }

    public string CallForPoints() {
        return Points switch {
            0 => "Love",
            1 => "Fifteen",
            2 => "Thirty",
            3 => "Forty"
        };
    }

    public string AllCall() {
        if (Points >= 3) return "Deuce";
        var callForPoints = CallForPoints();
        return $"{callForPoints}-All";
    }
}