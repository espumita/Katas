using MarsRover;
using MarsRover.Commands;

Console.WriteLine("Hello, World!");

var mapCoordinates = new MapCoordinates();
mapCoordinates.DetectObstacleAt(new Coordinates(0, 5));
mapCoordinates.DetectObstacleAt(new Coordinates(4, 0));
mapCoordinates.DetectObstacleAt(new Coordinates(-2, 2));
var router = new Router();
var marsRover = new MarsRover.MarsRover(mapCoordinates, router);

while (true) {
    PrintMarsMap(marsRover);
    Console.WriteLine("Available commands:");
    Console.WriteLine("⬆️ Move forward:");
    Console.WriteLine("➡️ Turn right:");
    Console.WriteLine("⬇️ Move backward:");
    Console.WriteLine("⬅️ Turn left:");
    Console.WriteLine("Next command:");
    var key = Console.ReadKey(true);

    marsRover.Execute(new List<IRobertCommand>{ CommandFrom(key.Key) });
}
IRobertCommand CommandFrom(ConsoleKey key) {
    return key switch {
        ConsoleKey.UpArrow => new MoveForwardCommand(),
        ConsoleKey.RightArrow => new TurnRightCommand(),
        ConsoleKey.DownArrow => new MoveBackwardCommand(),
        ConsoleKey.LeftArrow => new TurnLeftCommand(),
        _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
    };
}

void PrintMarsMap(MarsRover.MarsRover game) {
    Console.Clear();
    Console.OutputEncoding = System.Text.Encoding.UTF8;
for (int i = 5; i > -5; i--) {
        for (int j = -5; j < 5; j++) {
            if (game.Map.CurrentLocation.Equals(new Coordinates(j, i))) PrintRover(game.Router.CurrentDirection);
            else {
                if (game.Map.Obstacles.Contains(new Coordinates(j, i))) Console.Write("\ud83c\udf33");
                else Console.Write("\ud83d\udfe9");
            }
        }
        Console.Write("\n");
    }
}

void PrintRover(Direction direction) {
    if(direction == Direction.North) Console.Write("⬆️");
    if (direction == Direction.East) Console.Write("➡️");
    if (direction == Direction.South) Console.Write("⬇️");
    if (direction == Direction.West) Console.Write("⬅️");
}
