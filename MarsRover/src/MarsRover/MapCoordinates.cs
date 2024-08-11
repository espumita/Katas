namespace MarsRover;

public class MapCoordinates {
    public Coordinates CurrentLocation { get; private set; }
    public List<Coordinates> Obstacles { get; private set; }

    public MapCoordinates() {
        Obstacles = new List<Coordinates>();
    }

    public void StartUpRoverWithCurrentLocation(Coordinates coordinates) {
        CurrentLocation = coordinates;
    }

    public void UpdateCurrentLocation(Coordinates newLocation) {
        CurrentLocation = newLocation;
    }

    public void DetectObstacleAt(Coordinates coordinates) {
        Obstacles.Add(coordinates);
    }
}