namespace TicTacToe;

public class TicTacToeGame {
    private Board Board;
    private GameResult gameResult;

    public TicTacToeGame() {
        Board = new Board();
        gameResult = new GameResult(false, false, CellContent.Empty);
    }

    public void XPlaysIn(CellNumber cellNumber) {
        Board.PlayerPlays(cellNumber, CellContent.X);
        gameResult = CalculateGameResult();
    }

    public void OPlaysIn(CellNumber cellNumber) {
        Board.PlayerPlays(cellNumber, CellContent.O);
        gameResult = CalculateGameResult();
    }

    public CellContent Check(CellNumber cellNumber) {
        return Board.Check(cellNumber);
    }

    public GameResult GameResult() {
        return gameResult;
    }

    private GameResult CalculateGameResult() {
        if (PlayerIsWinner(CellContent.X)) return new GameResult(true, false, CellContent.X);
        if (PlayerIsWinner(CellContent.O)) return new GameResult(true, false, CellContent.O);
        if (Board.IsFull()) return new GameResult(true, true, CellContent.Empty);
        return new GameResult(false, false, CellContent.Empty);
    }

    private bool PlayerIsWinner(CellContent player) {
        return HasWinHorizontally(player)
            || HasWinVertically(player)
            || HasWinInDiagonal(player);
    }

    private bool HasWinHorizontally(CellContent player) {
        return HasWinInRow(CellNumber.One1, player)
            || HasWinInRow(CellNumber.Four4, player)
            || HasWinInRow(CellNumber.Seven7, player);
    }

    private bool HasWinInRow(CellNumber cellNumber, CellContent player) {
        return ExistsAndContains(cellNumber, player)
                && ExistsAndContains(cellNumber + 1, player)
                && ExistsAndContains(cellNumber + 2, player);
    }

    private bool ExistsAndContains(CellNumber cellNumber, CellContent player) {
        return Board.ExistsAndContains(cellNumber, player);
    }

    private bool HasWinVertically(CellContent player) {
        return HasWinInColumn(CellNumber.One1, player)
               || HasWinInColumn(CellNumber.Two2, player)
               || HasWinInColumn(CellNumber.Three3, player);
    }

    private bool HasWinInColumn(CellNumber cellNumber, CellContent player) {
        return ExistsAndContains(cellNumber, player)
               && ExistsAndContains(cellNumber + 3, player)
               && ExistsAndContains(cellNumber + 6, player);
    }

    private bool HasWinInDiagonal(CellContent player) {
        return HasWinInDiagonal(CellNumber.Five5, player, 2)
               || HasWinInDiagonal(CellNumber.Five5, player, 4);
    }

    private bool HasWinInDiagonal(CellNumber cellNumber, CellContent player, int delta) {
        return ExistsAndContains(cellNumber - delta, player)
               && ExistsAndContains(cellNumber, player)
               && ExistsAndContains(cellNumber + delta, player);
    }
}