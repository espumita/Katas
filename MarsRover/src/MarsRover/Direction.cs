namespace MarsRover;

public enum Direction {
    North,
    East,
    South,
    West
}

public static class DirectionExtensions {
    public static Direction TurnDirection(this Direction direction, Turn turn) => (direction, turn) switch {
        (Direction.North, Turn.Right) => Direction.East,
        (Direction.East,Turn.Right) => Direction.South,
        (Direction.South, Turn.Right) => Direction.West,
        (Direction.West, Turn.Right) => Direction.North,
        (Direction.North, Turn.Left) => Direction.West,
        (Direction.East, Turn.Left) => Direction.North,
        (Direction.South, Turn.Left) => Direction.East,
        (Direction.West, Turn.Left) => Direction.South
    };

}