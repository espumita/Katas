namespace MarsRover;

public record SimulatedExecutionResult(bool hasErrors = false, IEnumerable<Error> errors = null);