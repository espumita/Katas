namespace GameOfLife;

public record Coordinates(int X, int Y) {
    public List<Coordinates> Neighbours() {
        return new List<Coordinates> {
            new (X + 1, Y),
            new (X - 1, Y),
            new (X, Y + 1),
            new (X, Y - 1),
            new(X + 1, Y + 1),
            new (X - 1, Y - 1),
            new (X + 1, Y - 1),
            new (X -1, Y + 1)
        };
    }
}