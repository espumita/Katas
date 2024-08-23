namespace Minesweeper;

public class Board {
    private int maxRows;
    private int maxColumns;
    public IDictionary<Coordinates, CellType> Mines { get; private set; }

    public IDictionary<Coordinates, int> NearlyMines { get; private set; }

    public IDictionary<Coordinates, CellType> DugUpCells { get; private set; }

    public Board(int rows, int columns) {
        maxColumns = columns;
        maxRows = rows;
        Mines = new Dictionary<Coordinates, CellType>();
        NearlyMines = new Dictionary<Coordinates, int>();
        DugUpCells = new Dictionary<Coordinates, CellType>();
    }

    public void PlantMineAt(Coordinates coordinates) {
        Mines[coordinates] = CellType.Mine;
        UpdateNeighbourMineCount(coordinates);
    }

    public void DigUp(Coordinates coordinates, bool isPropagation = false) {
        if (DugUpCells.ContainsKey(coordinates)) return;
        if (Mines.ContainsKey(coordinates)) {
            if (!isPropagation) ExplodeAllMines();
            return;
        }
        if (NearlyMines.ContainsKey(coordinates)) {
            DugUpCells[coordinates] = CellTypeExtensions.CellTypeFromMinesNumber(NearlyMines[coordinates]);
        } else {
            DugUpCells[coordinates] = CellType.Empty;
            PropagateDigUpToNeighbours(coordinates);
        }
    }

    public bool HasGameEnd() {
        return
            Mines.Keys.Any(mine => DugUpCells.ContainsKey(mine))
            || DugUpCells.Count >= (maxRows * maxColumns) - Mines.Count;
    }

    private void UpdateNeighbourMineCount(Coordinates coordinates) {
        NearlyMines.Remove(coordinates);
        NeighboursInsideBoard(coordinates)
            .ForEach(neighbour => {
                if (Mines.ContainsKey(neighbour)) return;
                if (!NearlyMines.TryAdd(neighbour, 1)) {
                    NearlyMines[neighbour] += 1;
                }
            });
    }

    private List<Coordinates> NeighboursInsideBoard(Coordinates coordinates) {
        return coordinates.Neighbours()
            .Where(x => 
                x.X < maxRows
                && x.X >= 0
                && x.Y < maxColumns
                && x.Y >= 0
            ).ToList();
    }

    private void PropagateDigUpToNeighbours(Coordinates coordinates) {
        var neighbours = NeighboursInsideBoard(coordinates);
        neighbours
            .ForEach(neighbour => DigUp(neighbour, isPropagation: true));
    }

    private void ExplodeAllMines() {
        foreach (var mine in Mines.Keys) {
            DugUpCells[mine] = CellType.Mine;
        }
    }

    public CellType Check(Coordinates coordinates) {
        return DugUpCells.ContainsKey(coordinates)
            ? DugUpCells[coordinates]
            : CellType.Unknown;
    }


}