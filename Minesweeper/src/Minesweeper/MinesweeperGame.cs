namespace Minesweeper;

public class MinesweeperGame {

    public Board Board { get; private set; }
    public bool GameHasEnd { get; private set; }

    public MinesweeperGame(Board board) {
        this.Board = board;
    }

    public void DigUp(Coordinates coordinates) {
        Board.DigUp(coordinates);
        GameHasEnd = Board.HasGameEnd();
    }

    public CellType Check(Coordinates coordinates) {
        return Board.Check(coordinates);
    }

    public void PlantMineAt(Coordinates coordinates) {
        Board.PlantMineAt(coordinates);
    }

    public bool HasEnd() {
        return GameHasEnd;
    }
}