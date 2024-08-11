namespace MarsRover;

public record Coordinates(int X, int Y);

public static class CoordinatesExtensions {
    public static Coordinates Move(this Coordinates coordinates, Direction direction, Movement movement) => (direction, movement) switch {
        (Direction.North, Movement.Forward) => new Coordinates(coordinates.X, coordinates.Y + 1),
        (Direction.East, Movement.Forward) => new Coordinates(coordinates.X + 1, coordinates.Y),
        (Direction.South, Movement.Forward) => new Coordinates(coordinates.X, coordinates.Y - 1),
        (Direction.West, Movement.Forward) => new Coordinates(coordinates.X - 1, coordinates.Y),
        (Direction.North, Movement.Backward) => new Coordinates(coordinates.X, coordinates.Y - 1),
        (Direction.East, Movement.Backward) => new Coordinates(coordinates.X - 1, coordinates.Y),
        (Direction.South, Movement.Backward) => new Coordinates(coordinates.X, coordinates.Y + 1),
        (Direction.West, Movement.Backward) => new Coordinates(coordinates.X + 1, coordinates.Y)
    };

}