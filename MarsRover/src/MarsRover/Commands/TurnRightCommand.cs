namespace MarsRover.Commands;

public class TurnRightCommand : IRobertCommand {
    public SimulatedExecutionResult SimulateExecution(MapCoordinates map, Router router) {
        try {
            var newDirection = router.CurrentDirection.TurnDirection(Turn.Right);
            return new SimulatedExecutionResult();
        } catch (Exception exception) {
            return new SimulatedExecutionResult(hasErrors: true, new List<Error> { new Error(exception.Message) });
        }
    }

    public CommandExecutionResult Execute(MapCoordinates map, Router router) {
        TurnWells(Turn.Right);
        var newDirection = ReadNewFacingDirectionUsingSensors(router.CurrentDirection, Turn.Right);
        return new CommandExecutionResult(map.CurrentLocation, newDirection);
    }

    private void TurnWells(Turn turn) {
        //Simulated
    }

    private Direction ReadNewFacingDirectionUsingSensors(Direction direction, Turn turn) {
        return direction.TurnDirection(turn);
    }

}