using FluentAssertions;

namespace GameOfLife.Tests;

public class GameOfLifeTests {

    [Test]
    public void an_alone_cell_dies_by_underpopulation() {
        var world = new World();
        var game = new GameOfLife(world);
        var coordinates = new Coordinates(0, 0);
        game.PopulateCellAt(coordinates);

        game.EvolveGeneration();

        game.CellIsAliveAt(coordinates).Should().BeFalse();
    }

    [TestCase(1, 0)]
    [TestCase(-1, 0)]
    [TestCase(0, 1)]
    [TestCase(0, -1)]
    [TestCase(1, 1)]
    [TestCase(-1, -1)]
    [TestCase(1, -1)]
    [TestCase(-1, 1)]
    public void a_cell_with_a_single_neighbour_dies_by_underpopulation(int neighbourX, int neighbourY) {
        var world = new World();
        var game = new GameOfLife(world);
        var coordinates = new Coordinates(0, 0);
        game.PopulateCellAt(coordinates);
        game.PopulateCellAt(new Coordinates(neighbourX, neighbourY));

        game.EvolveGeneration();

        game.CellIsAliveAt(coordinates).Should().BeFalse();
    }

    [TestCase(1, 0)]
    [TestCase(-1, 0)]
    [TestCase(0, 1)]
    [TestCase(0, -1)]
    [TestCase(-1, -1)]
    [TestCase(1, -1)]
    [TestCase(-1, 1)]
    public void a_cell_with_two_neighbours_does_not_die_by_underpopulation(int neighbourX, int neighbourY) {
        var world = new World();
        var game = new GameOfLife(world);
        var coordinates = new Coordinates(0, 0);
        game.PopulateCellAt(coordinates);
        game.PopulateCellAt(new Coordinates(neighbourX, neighbourY));
        game.PopulateCellAt(new Coordinates(1, 1));

        game.EvolveGeneration();

        game.CellIsAliveAt(coordinates).Should().BeTrue();
    }


    [Test]
    public void a_cell_with_more_than_tree_neighbours_dies_by_overcrowding() {
        var world = new World();
        var game = new GameOfLife(world);
        var coordinates = new Coordinates(0, 0);
        game.PopulateCellAt(coordinates);
        game.PopulateCellAt(new Coordinates(1, 0));
        game.PopulateCellAt(new Coordinates(-1, 0));
        game.PopulateCellAt(new Coordinates(0, 1));
        game.PopulateCellAt(new Coordinates(0, -1));

        game.EvolveGeneration();

        game.CellIsAliveAt(coordinates).Should().BeFalse();
    }

    [Test]
    public void a_cell_with_tree_neighbours_does_not_die() {
        var world = new World();
        var game = new GameOfLife(world);
        var coordinates = new Coordinates(0, 0);
        game.PopulateCellAt(coordinates);
        game.PopulateCellAt(new Coordinates(1, 0));
        game.PopulateCellAt(new Coordinates(-1, 0));
        game.PopulateCellAt(new Coordinates(0, 1));

        game.EvolveGeneration();

        game.CellIsAliveAt(coordinates).Should().BeTrue();
    }

    [Test]
    public void a_dead_cell_with_tree_neighbours_born() {
        var world = new World();
        var game = new GameOfLife(world);
        var coordinates = new Coordinates(0, 0);
        game.PopulateCellAt(new Coordinates(1, 0));
        game.PopulateCellAt(new Coordinates(-1, 0));
        game.PopulateCellAt(new Coordinates(0, 1));

        game.EvolveGeneration();

        game.CellIsAliveAt(coordinates).Should().BeTrue();
    }
}