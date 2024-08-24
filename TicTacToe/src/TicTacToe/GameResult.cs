namespace TicTacToe;

public record GameResult(bool gameHasFinished, bool isDraw, CellContent winner);