
using GameOfLife;

var world = new World();
var gameOfLife = new GameOfLife.GameOfLife(world);

gameOfLife.PopulateCellAt(new Coordinates(0, 1));
gameOfLife.PopulateCellAt(new Coordinates(1, 0));
gameOfLife.PopulateCellAt(new Coordinates(0, -1));
gameOfLife.PopulateCellAt(new Coordinates(-1, -1));
gameOfLife.PopulateCellAt(new Coordinates(1, -1));

var generation = 0;
while (generation <= 50) {
    PrintWorld(gameOfLife);
    gameOfLife.EvolveGeneration();
    Console.WriteLine($"Generation: {generation}");
    Console.WriteLine($"Population: {gameOfLife.World.Cells.Count}");
    Thread.Sleep(TimeSpan.FromMilliseconds(100));
    generation++;
}

void PrintWorld(GameOfLife.GameOfLife game) {
    Console.Clear();
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    for (int i = 10; i > -10; i--) {
        for (int j = -10; j < 10; j++) {
            if (game.World.Cells.ContainsKey(new Coordinates(j, i))) Console.Write("⬛");
            else Console.Write("⬜");
        }
        Console.Write("\n");
    }
}

