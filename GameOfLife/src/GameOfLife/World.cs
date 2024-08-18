namespace GameOfLife;

public class World {
    public IDictionary<Coordinates, List<Coordinates>> Cells { get; private set; }
    private IDictionary<Coordinates, int> neighboursWithCounter;

    public World() {
        Cells = new Dictionary<Coordinates, List<Coordinates>>();
        neighboursWithCounter = new Dictionary<Coordinates, int>();
    }

    public void AddCellAt(Coordinates coordinates) {
        var neighbours = coordinates.Neighbours();
        Cells[coordinates] = neighbours;
        IncreaseNeighboursCount(neighbours);
    }

    public void Kill(List<Coordinates> cellsToKill) {
        cellsToKill.ForEach(cellToKill => {
            Cells.Remove(cellToKill);
            var neighbours = cellToKill.Neighbours();
            DecreaseNeighboursCount(neighbours);
        });
    }

    public bool IsAlive(Coordinates coordinates) {
        return Cells.ContainsKey(coordinates);
    }

    public List<Coordinates> CellsThatHaveToDie() {
        return Cells.Keys.Where(key => {
            var aliveNeighbours = AliveNeighbours(neighbours: Cells[key], aliveCells: Cells.Keys);
            return Underpopulation(aliveNeighbours.Count())
                   || Overcrowding(aliveNeighbours.Count());
        }).ToList();
    }

    public List<Coordinates> CellsThatHaveToBorn() {
        return neighboursWithCounter.Keys
            .Where(cell =>
                !IsAlive(cell)
                && HasEnoughNeighboursToBorn(cell)
            ).ToList();
    }

    private void IncreaseNeighboursCount(List<Coordinates> neighbours) {
        neighbours.ForEach(neighbour => {
            if (!neighboursWithCounter.TryAdd(neighbour, 1)) {
                neighboursWithCounter[neighbour] += 1;
            }
        });
    }

    private void DecreaseNeighboursCount(List<Coordinates> neighbours) {
        neighbours.ForEach(neighbour => {
            if (neighboursWithCounter.ContainsKey(neighbour)) {
                neighboursWithCounter[neighbour] -= 1;
            } else {
                neighboursWithCounter.Remove(neighbour);
            }
        });
    }

    private static IEnumerable<Coordinates> AliveNeighbours(List<Coordinates> neighbours, IEnumerable<Coordinates> aliveCells) {
        return neighbours.Where(aliveCells.Contains);
    }

    private static bool Underpopulation(int neighboursCount) {
        return neighboursCount < 2;
    }

    private static bool Overcrowding(int neighboursCount) {
        return neighboursCount > 3;
    }

    private bool HasEnoughNeighboursToBorn(Coordinates coordinates) {
        return neighboursWithCounter[coordinates] == 3;
    }
}