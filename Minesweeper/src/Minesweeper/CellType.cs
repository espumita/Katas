namespace Minesweeper;

public enum CellType {
    Unknown,
    Empty,
    Mine,
    NearlyMines1,
    NearlyMines2,
    NearlyMines3,
    NearlyMines4,
    NearlyMines5,
    NearlyMines6,
    NearlyMines7,
    NearlyMines8
}

public static class CellTypeExtensions {
    public static CellType CellTypeFromMinesNumber(int nearlyMine) => nearlyMine switch {
        1 => CellType.NearlyMines1,
        2 => CellType.NearlyMines2,
        3 => CellType.NearlyMines3,
        4 => CellType.NearlyMines4,
        5 => CellType.NearlyMines5,
        6 => CellType.NearlyMines6,
        7 => CellType.NearlyMines7,
        8 => CellType.NearlyMines8
    };

}
