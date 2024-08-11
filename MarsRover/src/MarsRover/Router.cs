namespace MarsRover;

public class Router {

    public Direction CurrentDirection { get; private set; }

    public void StartUpRoverWithCurrentDirection(Direction direction) {
        CurrentDirection = direction;
    }

    public void UpdateCurrentDirection(Direction direction) {
        CurrentDirection = direction;
    }
}