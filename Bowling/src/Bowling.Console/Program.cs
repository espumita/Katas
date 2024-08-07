using Bowling;
using System.Numerics;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Welcome to Bowling game! 🎳");
Console.Write("Number of players: ");
var numberOfPlayers = int.Parse(Console.ReadLine());

var players = new List<Player>();

foreach (var playerNumber in Enumerable.Range(1, numberOfPlayers)) {
    Console.Write($"Player {playerNumber} name: ");
    var playerName = Console.ReadLine();
    var player = new Player(playerName);
    players.Add(player);
}

var bowling = new BowlingGame(players);

while (true) {
    foreach (var player in players) {
        PrintBoard(bowling);
        Console.Write($"Roll for player {player.name}: ");
        int knockDownPins = int.Parse(Console.ReadLine());

        bowling.Roll(player, knockDownPins);
    }

    if (bowling.GameEnds()) {
        PrintBoard(bowling);
        Console.WriteLine("Game ends!");
        break;
    }
}

void PrintBoard(BowlingGame game) {
    Console.Clear();

    Console.Write(FormatColumn("Player", 10));

    for (int i = 1; i <= 10; i++) {
        Console.Write(FormatColumn($"|{i}   "));
    }
    Console.Write("  |");
    Console.WriteLine();
    Console.Write(FormatColumn(string.Empty, 10));

    for (int i = 1; i <= 10; i++) {
        if (i == 10) Console.Write("|1 2 3 ");
        else Console.Write(FormatColumn("|1 2 "));
    }
    Console.Write("|");
    Console.WriteLine();

    foreach (var player in game.Scores.Keys) {
        Console.Write("----------");
        for (int i = 0; i < 10; i++) {
            Console.Write("-----");
        }
        Console.Write("---");
        Console.WriteLine();
        Console.Write(FormatColumn(player.name, 10));
        for (int i = 0; i < 10; i++) {
            if (i == 9) Console.Write(FormatColumn($"| {PrintRoll(game, player, i)}", 7));
            else Console.Write(FormatColumn($"| {PrintRoll(game, player, i)}"));
        }
        Console.Write("|");
        Console.WriteLine();
        Console.Write(FormatColumn(string.Empty, 10));

        for (int i = 0; i < 10; i++) {
            if (i == 9) Console.Write(FormatColumn($"|{PrintScore(game, player, i)}", 7));
            else Console.Write(FormatColumn($"|{PrintScore(game, player, i)}  "));
        }
        Console.Write("|");
        Console.WriteLine();
    }
}

string PrintRoll(BowlingGame bowlingGame, Player? player, int frameIndex) {
    var frame = bowlingGame.Scores[player].Frames[frameIndex];
    if (frame.IsLastFrame()) {
        if (frame.Rolls.Count == 3) return $"{Format(frame.Rolls[0].KnockDownPins)}|{Format(frame.Rolls[1].KnockDownPins)}|{Format(frame.Rolls[2].KnockDownPins)}";
        if (frame.Rolls.Count == 2) return $"{Format(frame.Rolls[0].KnockDownPins)}|{Format(frame.Rolls[1].KnockDownPins)}| ";
        if (frame.Rolls.Count == 1) return $"{Format(frame.Rolls[0].KnockDownPins)}| | ";
        if (frame.Rolls.Count == 0) return " | | ";
    }
    if (frame.Status is FrameStatus.Strike) return "X| ";
    if (frame.Status is FrameStatus.Spare) return $"{frame.Rolls[0].KnockDownPins}|/";
    if (frame.Status is FrameStatus.Completed) return $"{Format(frame.Rolls[0].KnockDownPins)}|{Format(frame.Rolls[1].KnockDownPins)}";
    if (frame.Status is FrameStatus.InProgress && frame.Rolls.Count >= 1) return $"{frame.Rolls[0].KnockDownPins}| ";
    return " | ";
}

static string FormatColumn(string input, int number = 5) {
    return input.Length > number 
        ? input.Substring(0, number) 
        : input.PadRight(number);
}

string PrintScore(BowlingGame bowlingGame, Player? player, int frameIndex) {
    if (frameIndex > bowlingGame.Scores[player].CurrentFrame) return "";
    var accumulatedScore = bowlingGame.Scores[player].Frames
        .Where(frame => frame.Number <= frameIndex)
        .Sum(frame => frame.LastCalculatedScore);
    return accumulatedScore != 0 ? accumulatedScore.ToString() : "" ;
}

string Format(int pins) {
    return pins switch {
        10 => "X",
        0 => "-",
        _ => pins.ToString()
    };
}