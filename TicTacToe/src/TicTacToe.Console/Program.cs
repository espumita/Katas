
using TicTacToe;

var game = new TicTacToeGame();

var playerXTurn = true;
while (!game.GameResult().gameHasFinished) {
    PrintWorld(game);
    if (playerXTurn) {
        Console.WriteLine("Player X to dig up:");
        var cellNumber = int.Parse(Console.ReadLine());
        game.XPlaysIn((CellNumber)(cellNumber - 1));
        playerXTurn = !playerXTurn;
    } else {
        Console.WriteLine("Player O to dig up:");
        var cellNumber = int.Parse(Console.ReadLine());
        game.OPlaysIn((CellNumber)(cellNumber - 1));
        playerXTurn = !playerXTurn;
    }

}
PrintWorld(game);
Console.WriteLine("Game ends!");
if (game.GameResult().isDraw) {
    Console.WriteLine("Draw!");
} else {
    Console.WriteLine($"Player {game.GameResult().winner} Wins!");
}



void PrintWorld(TicTacToeGame game) {
    Console.Clear();
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    Console.WriteLine("+---+---+---+");
    Console.WriteLine($"| {PrintCellContent(1, game)} | {PrintCellContent(2, game)} | {PrintCellContent(3, game)} |");
    Console.WriteLine("+---+---+---+");
    Console.WriteLine($"| {PrintCellContent(4, game)} | {PrintCellContent(5, game)} | {PrintCellContent(6, game)} |");
    Console.WriteLine("+---+---+---+");
    Console.WriteLine($"| {PrintCellContent(7, game)} | {PrintCellContent(8, game)} | {PrintCellContent(9, game)} |");
    Console.WriteLine("+---+---+---+");
}

string PrintCellContent(int i, TicTacToeGame game) {
    var cellType = game.Check((CellNumber)i - 1);
    return cellType switch {
        CellContent.Empty => i.ToString(),
        CellContent.X => "X",
        CellContent.O => "O"
    };
}