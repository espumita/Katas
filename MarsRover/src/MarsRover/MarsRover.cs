using MarsRover.Commands;

namespace MarsRover;

public class MarsRover {
    public MapCoordinates Map { get; private set; }
    public Router Router { get; private set; }

    public MarsRover(MapCoordinates mapCoordinates, Router router) {
        Map = mapCoordinates;
        Router = router;
        Map.StartUpRoverWithCurrentLocation(new Coordinates(0, 0));
        Router.StartUpRoverWithCurrentDirection(Direction.North);
    }

    public CommandExecutionsResult Execute(List<IRobertCommand> commands) {
        foreach (var command in commands) {
            var simulateExecution = command.SimulateExecution(Map, Router);
            if (simulateExecution.hasErrors) {
                return new CommandExecutionsResult(hasErrors: true, simulateExecution.errors);
            }
            var result = command.Execute(Map, Router);
            Map.UpdateCurrentLocation(result.coordinates);
            Router.UpdateCurrentDirection(result.direction);
        }
        return new CommandExecutionsResult();
    }
}

public record CommandExecutionsResult(bool hasErrors = false, IEnumerable<Error> erros = null);

public record Error(string description);