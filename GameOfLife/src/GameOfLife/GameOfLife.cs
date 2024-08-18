namespace GameOfLife;

public class GameOfLife {
    public World World { get; private set; }

    public GameOfLife(World world) {
        World = world;
    }

    public void PopulateCellAt(Coordinates coordinates) {
        World.AddCellAt(coordinates);
    }

    public bool CellIsAliveAt(Coordinates coordinates) {
        return World.IsAlive(coordinates);
    }

    public void EvolveGeneration() {
        var cellsThatHaveToDie = CellsThatHaveToDie();
        var cellsThatHaveToBorn = CellsThatHaveToBorn();
        Kill(cellsThatHaveToDie);
        Born(cellsThatHaveToBorn);
    }

    private List<Coordinates> CellsThatHaveToDie() {
        return World.CellsThatHaveToDie();
    }

    private List<Coordinates> CellsThatHaveToBorn() {
        return World.CellsThatHaveToBorn();
    }

    private void Kill(List<Coordinates> cells) {
        World.Kill(cells);
    }

    private void Born(List<Coordinates> cells) {
        cells.ForEach(cell => World.AddCellAt(cell));
    }
}