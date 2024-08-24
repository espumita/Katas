namespace TicTacToe;

public class Board {
    private Dictionary<CellNumber, CellContent> Cells;

    public Board() {
        Cells = new Dictionary<CellNumber, CellContent>();
    }

    public void PlayerPlays(CellNumber cellNumber, CellContent cellContent) {
        Cells[cellNumber] = cellContent;
    }

    public CellContent Check(CellNumber cellNumber) {
        return Cells.ContainsKey(cellNumber)
            ? Cells[cellNumber]
            : CellContent.Empty;
    }

    public bool IsFull() {
        return Cells.Count == 9;
    }

    public bool ExistsAndContains(CellNumber cellNumber, CellContent player) {
        return Cells.ContainsKey(cellNumber)
               && Cells[cellNumber] == player;
    }
}