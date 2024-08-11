namespace MarsRover.Commands;

public interface IRobertCommand {
    SimulatedExecutionResult SimulateExecution(MapCoordinates map, Router router);
    CommandExecutionResult Execute(MapCoordinates map, Router router);
}