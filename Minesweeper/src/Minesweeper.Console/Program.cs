using Minesweeper;

var size = 10;
var numberOfMines = 4;
var world = new Board(size, size);
var game = new MinesweeperGame(world);

foreach (var mine in Enumerable.Range(0, numberOfMines)) {
    var randomCoordinates = RandomCoordinates(size);
    game.PlantMineAt(randomCoordinates);
}

while (!game.HasEnd()) {
    PrintWorld(game);
    Console.WriteLine($"Mines: {game.Board.Mines.Count}");
    Console.WriteLine("Coordiante X to dig up:");
    var x = int.Parse(Console.ReadLine());
    Console.WriteLine("Coordiante Y to dig up:");
    var y = int.Parse(Console.ReadLine());
    game.DigUp(new Coordinates(x, y));
}
PrintWorld(game);
Console.WriteLine("Game ends!");

void PrintWorld(MinesweeperGame game) {
    Console.Clear();
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    for (int i = size - 1; i >= 0; i--) {
        for (int j = 0; j < size; j++) {
            var cellType = game.Check(new Coordinates(j, i));
            Console.Write(PrintCell(cellType));
        }
        Console.Write("\n");
    }
}

string PrintCell(CellType cellType) {
    return cellType switch {
        CellType.Unknown => "⬛",
        CellType.Empty => "⬜",
        CellType.Mine => "💣",
        CellType.NearlyMines1 => "😀",
        CellType.NearlyMines2 => "🙂",
        CellType.NearlyMines3 => "😐",
        CellType.NearlyMines4 => "🤨",
        CellType.NearlyMines5 => "🥲",
        CellType.NearlyMines6 => "😥",
        CellType.NearlyMines7 => "😓",
        CellType.NearlyMines8 => "🙃"
    };
}

Coordinates RandomCoordinates(int maxValue) {
    var random = new Random();
    var x = random.Next(0, maxValue);
    var y = random.Next(0, maxValue);
    return new Coordinates(x, y);
}
