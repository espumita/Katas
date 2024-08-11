namespace MarsRover.Commands;

public class MoveForwardCommand : IRobertCommand {
    public SimulatedExecutionResult SimulateExecution(MapCoordinates map, Router router) {
        try {
            var newLocation = map.CurrentLocation.Move(router.CurrentDirection, Movement.Forward); ;
            if (map.Obstacles.Contains(newLocation)) throw new Exception($"Obstacle found at ({newLocation.X},{newLocation.Y})");
            return new SimulatedExecutionResult();
        } catch (Exception exception) {
            return new SimulatedExecutionResult(hasErrors: true, new List<Error> { new Error(exception.Message) });
        }
    }

    public CommandExecutionResult Execute(MapCoordinates map, Router router) {
        MoveWheels(Movement.Forward);
        var newLocation = ReadNewLocationUsingSensors(map.CurrentLocation, router.CurrentDirection, Movement.Forward);
        return new CommandExecutionResult(newLocation, router.CurrentDirection);
    }

    private void MoveWheels(Movement movement) {
        //Simulated
    }

    private Coordinates ReadNewLocationUsingSensors(Coordinates lastLocation, Direction direction, Movement movement) {
        return lastLocation.Move(direction, movement);
    }
}