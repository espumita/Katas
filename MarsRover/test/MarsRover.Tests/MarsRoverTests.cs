using FluentAssertions;
using MarsRover.Commands;

namespace MarsRover.Tests;

public class MarsRoverTests {
    private MapCoordinates mapCoordinates;
    private Router router;
    private MarsRover marsRover;

    [SetUp]
    public void SetUp() {
        mapCoordinates = new MapCoordinates();
        router = new Router();
        marsRover = new MarsRover(mapCoordinates, router);
    }

    [Test]
    public void starts_in_a_start_point_with_a_direction() {

        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(0, 0));
        marsRover.Router.CurrentDirection.Should().Be(Direction.North);
    }

    [Test]
    public void turn_right() {

        var command = new TurnRightCommand();

        marsRover.Execute(new List<IRobertCommand>{ command });
        marsRover.Router.CurrentDirection.Should().Be(Direction.East);
        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(0, 0));

        marsRover.Execute(new List<IRobertCommand> { command });
        marsRover.Router.CurrentDirection.Should().Be(Direction.South);
        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(0, 0));

        marsRover.Execute(new List<IRobertCommand> { command });
        marsRover.Router.CurrentDirection.Should().Be(Direction.West);
        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(0, 0));

        marsRover.Execute(new List<IRobertCommand> { command });
        marsRover.Router.CurrentDirection.Should().Be(Direction.North);
        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(0, 0));
    }

    [Test]
    public void turn_left() {

        var command = new TurnLeftCommand();

        marsRover.Execute(new List<IRobertCommand> { command });
        marsRover.Router.CurrentDirection.Should().Be(Direction.West);
        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(0, 0));

        marsRover.Execute(new List<IRobertCommand> { command });
        marsRover.Router.CurrentDirection.Should().Be(Direction.South);
        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(0, 0));

        marsRover.Execute(new List<IRobertCommand> { command });
        marsRover.Router.CurrentDirection.Should().Be(Direction.East);
        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(0, 0));

        marsRover.Execute(new List<IRobertCommand> { command });
        marsRover.Router.CurrentDirection.Should().Be(Direction.North);
        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(0, 0));
    }

    [TestCase(Direction.North, 0, 1)]
    public void move_forward_facing_north(Direction direction, int x, int y) {
        var command = new MoveForwardCommand();
        
        marsRover.Execute(new List<IRobertCommand>{ command });

        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(x, y));
        marsRover.Router.CurrentDirection.Should().Be(direction);
    }

    [TestCase(Direction.East, 1, 0)]
    public void move_forward_facing_east(Direction direction, int x, int y) {
        var command = new MoveForwardCommand();

        marsRover.Execute(new List<IRobertCommand> {
            new TurnRightCommand(),
            command
        });

        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(x, y));
        marsRover.Router.CurrentDirection.Should().Be(direction);
    }

    [TestCase(Direction.South, 0, -1)]
    public void move_forward_facing_south(Direction direction, int x, int y) {
        var command = new MoveForwardCommand();

        marsRover.Execute(new List<IRobertCommand> {
            new TurnRightCommand(),
            new TurnRightCommand(),
            command
        });

        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(x, y));
        marsRover.Router.CurrentDirection.Should().Be(direction);
    }

    [TestCase(Direction.West, -1, 0)]
    public void move_forward_facing_west(Direction direction, int x, int y) {
        var command = new MoveForwardCommand();

        marsRover.Execute(new List<IRobertCommand> {
            new TurnLeftCommand(),
            command
        });

        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(x, y));
        marsRover.Router.CurrentDirection.Should().Be(direction);
    }

    [TestCase(Direction.North, 0, -1)]
    public void move_backward_facing_north(Direction direction, int x, int y) {
        var command = new MoveBackwardCommand();

        marsRover.Execute(new List<IRobertCommand> { command });

        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(x, y));
        marsRover.Router.CurrentDirection.Should().Be(direction);
    }

    [TestCase(Direction.East, -1, 0)]
    public void move_backward_facing_east(Direction direction, int x, int y) {
        var command = new MoveBackwardCommand();

        marsRover.Execute(new List<IRobertCommand> {
            new TurnRightCommand(),
            command
        });

        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(x, y));
        marsRover.Router.CurrentDirection.Should().Be(direction);
    }

    [TestCase(Direction.South, 0, +1)]
    public void move_backward_facing_south(Direction direction, int x, int y) {
        var command = new MoveBackwardCommand();

        marsRover.Execute(new List<IRobertCommand> {
            new TurnRightCommand(),
            new TurnRightCommand(),
            command
        });

        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(x, y));
        marsRover.Router.CurrentDirection.Should().Be(direction);
    }

    [TestCase(Direction.West, +1, 0)]
    public void move_backward_facing_west(Direction direction, int x, int y) {
        var command = new MoveBackwardCommand();

        marsRover.Execute(new List<IRobertCommand> {
            new TurnLeftCommand(),
            command
        });

        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(x, y));
        marsRover.Router.CurrentDirection.Should().Be(direction);
    }

    [Test]
    public void move_without_problems_when_there_is_no_obstacles() {

        var result = marsRover.Execute(new List<IRobertCommand> {
            new TurnRightCommand(),
            new MoveForwardCommand(),
            new MoveForwardCommand(),
            new MoveForwardCommand(),
        });

        result.hasErrors.Should().BeFalse();
        result.erros.Should().BeNull();
        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(3, 0));
        marsRover.Router.CurrentDirection.Should().Be(Direction.East);
    }

    [Test]
    public void do_not_execute_more_commands_if_there_is_an_obstacle_in_the_route() {
        mapCoordinates.DetectObstacleAt(new Coordinates(2, 0));

        var result = marsRover.Execute(new List<IRobertCommand> {
            new TurnRightCommand(),
            new MoveForwardCommand(),
            new MoveForwardCommand(),
            new MoveForwardCommand(),
        });

        result.hasErrors.Should().BeTrue();
        result.erros.Single().Should().Be(new Error("Obstacle found at (2,0)"));
        marsRover.Map.CurrentLocation.Should().Be(new Coordinates(1, 0));
        marsRover.Router.CurrentDirection.Should().Be(Direction.East);
    }
}



