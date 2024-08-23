using FluentAssertions;

namespace Minesweeper.Tests;

public class MinesweeperGameTests {
    private const int SomeRows = 3;
    private const int SomeColumns = 3;
    private MinesweeperGame game;
    private Board Board;

    [SetUp]

    public void SetUp() {
        Board = new Board(SomeRows, SomeColumns);
        game = new MinesweeperGame(Board);
    }

    [Test]
    public void when_start_the_game_everything_is_unknown() {

        ValidateAllCellsAre(CellType.Unknown);
    }

    [TestCase(0,0)]
    [TestCase(1,0)]
    [TestCase(0,1)]
    [TestCase(1,1)]
    public void when_to_dig_up_an_empty_cell_show_all_the_empty_places(int x, int y) {

        game.DigUp(new Coordinates(x, y));

        ValidateAllCellsAre(CellType.Empty);
    }

    [Test]
    public void show_number_of_nearly_mines_when_there_is_one() {
        game.PlantMineAt(new Coordinates(0, 0));

        game.DigUp(new Coordinates(2, 2));

        Board.Mines.Count.Should().Be(1);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Unknown);
        Board.NearlyMines.Count.Should().Be(3);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.NearlyMines1);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.NearlyMines1);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.NearlyMines1);
        Board.DugUpCells.Count.Should().Be(8);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.Empty);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Empty);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.Empty);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Empty);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Empty);
    }

    [Test]
    public void show_number_of_nearly_mines_when_there_is_one_and_no_empty_cells() {
        game.PlantMineAt(new Coordinates(1, 1));

        game.DigUp(new Coordinates(2, 2));

        Board.Mines.Count.Should().Be(1);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Unknown);
        Board.NearlyMines.Count.Should().Be(8);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Unknown);
        Board.DugUpCells.Count.Should().Be(1);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.NearlyMines1);
    }

    [Test]
    public void show_number_of_nearly_mines_when_there_two_mines_separated() {
        game.PlantMineAt(new Coordinates(0, 0));
        game.PlantMineAt(new Coordinates(2, 2));

        game.DigUp(new Coordinates(1, 1));

        Board.Mines.Count.Should().Be(2);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.Unknown);
        Board.NearlyMines.Count.Should().Be(5);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Unknown);
        Board.DugUpCells.Count.Should().Be(1);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.NearlyMines2);
    }

    [Test]
    public void show_number_of_nearly_mines_when_there_two_mines_together() {
        game.PlantMineAt(new Coordinates(0, 0));
        game.PlantMineAt(new Coordinates(1, 0));

        game.DigUp(new Coordinates(2, 2));

        Board.Mines.Count.Should().Be(2);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.Unknown);
        Board.NearlyMines.Count.Should().Be(4);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.NearlyMines2);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.NearlyMines2);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.NearlyMines1);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Unknown);
        Board.DugUpCells.Count.Should().Be(6);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.Empty);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Empty);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Empty);
    }

    [Test]
    public void show_number_of_nearly_mines_when_there_tree_mines_together() {
        game.PlantMineAt(new Coordinates(0, 0));
        game.PlantMineAt(new Coordinates(1, 0));
        game.PlantMineAt(new Coordinates(0, 1));

        game.DigUp(new Coordinates(1, 1));

        Board.Mines.Count.Should().Be(3);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.Unknown);
        Board.NearlyMines.Count.Should().Be(5);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.Unknown);
        Board.DugUpCells.Count.Should().Be(1);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.NearlyMines3);
    }

    [Test]
    public void show_number_of_nearly_mines_when_there_four_mines_together() {
        game.PlantMineAt(new Coordinates(0, 0));
        game.PlantMineAt(new Coordinates(1, 0));
        game.PlantMineAt(new Coordinates(0, 1));
        game.PlantMineAt(new Coordinates(0, 2));

        game.DigUp(new Coordinates(1, 1));

        Board.Mines.Count.Should().Be(4);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Unknown);
        Board.NearlyMines.Count.Should().Be(4);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.Unknown);
        Board.DugUpCells.Count.Should().Be(1);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.NearlyMines4);
    }

    [Test]
    public void show_number_of_nearly_mines_when_there_five_mines_together() {
        game.PlantMineAt(new Coordinates(0, 0));
        game.PlantMineAt(new Coordinates(1, 0));
        game.PlantMineAt(new Coordinates(0, 1));
        game.PlantMineAt(new Coordinates(0, 2));
        game.PlantMineAt(new Coordinates(2, 0));

        game.DigUp(new Coordinates(1, 1));

        Board.Mines.Count.Should().Be(5);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Unknown);
        Board.NearlyMines.Count.Should().Be(3);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.Unknown);
        Board.DugUpCells.Count.Should().Be(1);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.NearlyMines5);
    }

    [Test]
    public void show_number_of_nearly_mines_when_there_six_mines_together() {
        game.PlantMineAt(new Coordinates(0, 0));
        game.PlantMineAt(new Coordinates(1, 0));
        game.PlantMineAt(new Coordinates(0, 1));
        game.PlantMineAt(new Coordinates(0, 2));
        game.PlantMineAt(new Coordinates(2, 0));
        game.PlantMineAt(new Coordinates(1, 2));

        game.DigUp(new Coordinates(1, 1));

        Board.Mines.Count.Should().Be(6);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Unknown);
        Board.NearlyMines.Count.Should().Be(3);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.Unknown);
        Board.DugUpCells.Count.Should().Be(1);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.NearlyMines6);
    }

    [Test]
    public void show_number_of_nearly_mines_when_there_seven_mines_together() {
        game.PlantMineAt(new Coordinates(0, 0));
        game.PlantMineAt(new Coordinates(1, 0));
        game.PlantMineAt(new Coordinates(0, 1));
        game.PlantMineAt(new Coordinates(0, 2));
        game.PlantMineAt(new Coordinates(2, 0));
        game.PlantMineAt(new Coordinates(1, 2));
        game.PlantMineAt(new Coordinates(2, 1));

        game.DigUp(new Coordinates(1, 1));

        Board.Mines.Count.Should().Be(7);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.Unknown);
        Board.NearlyMines.Count.Should().Be(2);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.Unknown);
        Board.DugUpCells.Count.Should().Be(1);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.NearlyMines7);
    }

    [Test]
    public void show_number_of_nearly_mines_when_there_eight_mines_together() {
        game.PlantMineAt(new Coordinates(0, 0));
        game.PlantMineAt(new Coordinates(1, 0));
        game.PlantMineAt(new Coordinates(0, 1));
        game.PlantMineAt(new Coordinates(0, 2));
        game.PlantMineAt(new Coordinates(2, 0));
        game.PlantMineAt(new Coordinates(1, 2));
        game.PlantMineAt(new Coordinates(2, 1));
        game.PlantMineAt(new Coordinates(2, 2));

        game.DigUp(new Coordinates(1, 1));

        Board.Mines.Count.Should().Be(8);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.Unknown);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.Unknown);
        Board.NearlyMines.Count.Should().Be(1);
        Board.DugUpCells.Count.Should().Be(1);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.NearlyMines8);
    }

    [Test]
    public void full_of_mines_when_you_touch_a_mine() {
        game.PlantMineAt(new Coordinates(0, 0));
        game.PlantMineAt(new Coordinates(1, 0));
        game.PlantMineAt(new Coordinates(0, 1));
        game.PlantMineAt(new Coordinates(0, 2));
        game.PlantMineAt(new Coordinates(2, 0));
        game.PlantMineAt(new Coordinates(1, 2));
        game.PlantMineAt(new Coordinates(2, 1));
        game.PlantMineAt(new Coordinates(2, 2));
        game.PlantMineAt(new Coordinates(1, 1));

        game.DigUp(new Coordinates(1, 1));

        Board.Mines.Count.Should().Be(9);
        game.Check(new Coordinates(0, 0)).Should().Be(CellType.Mine);
        game.Check(new Coordinates(1, 0)).Should().Be(CellType.Mine);
        game.Check(new Coordinates(0, 1)).Should().Be(CellType.Mine);
        game.Check(new Coordinates(0, 2)).Should().Be(CellType.Mine);
        game.Check(new Coordinates(2, 0)).Should().Be(CellType.Mine);
        game.Check(new Coordinates(1, 2)).Should().Be(CellType.Mine);
        game.Check(new Coordinates(2, 1)).Should().Be(CellType.Mine);
        game.Check(new Coordinates(2, 2)).Should().Be(CellType.Mine);
        game.Check(new Coordinates(1, 1)).Should().Be(CellType.Mine);
        Board.NearlyMines.Count.Should().Be(0);
        Board.DugUpCells.Count.Should().Be(9);
    }

    private void ValidateAllCellsAre(CellType expectedCellType) {
        for (int x = 0; x < SomeRows; x++) {
            for (int y = 0; y < SomeColumns; y++) {
                var cellAt = game.Check(new Coordinates(x, y));
                cellAt.Should().Be(expectedCellType);
            }
        }
    }
}