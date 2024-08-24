using FluentAssertions;

namespace TicTacToe.Tests;

public class TicTacToeGameTests {

    [Test]
    public void game_starts_with_empty_board() {
        var game = new TicTacToeGame();

        game.Check(CellNumber.One1).Should().Be(CellContent.Empty);
        game.Check(CellNumber.Two2).Should().Be(CellContent.Empty);
        game.Check(CellNumber.Three3).Should().Be(CellContent.Empty);
        game.Check(CellNumber.Four4).Should().Be(CellContent.Empty);
        game.Check(CellNumber.Five5).Should().Be(CellContent.Empty);
        game.Check(CellNumber.Six6).Should().Be(CellContent.Empty);
        game.Check(CellNumber.Seven7).Should().Be(CellContent.Empty);
        game.Check(CellNumber.Eight8).Should().Be(CellContent.Empty);
        game.Check(CellNumber.Nine9).Should().Be(CellContent.Empty);
        game.GameResult().gameHasFinished.Should().BeFalse();
    }

    [TestCase(CellNumber.One1, CellNumber.Two2, CellNumber.Three3)]
    [TestCase(CellNumber.Four4, CellNumber.Five5, CellNumber.Six6)]
    [TestCase(CellNumber.Seven7, CellNumber.Eight8, CellNumber.Nine9)]
    public void x_player_wins_horizontally(CellNumber number1, CellNumber number2, CellNumber number3) {
        var game = new TicTacToeGame();

        game.XPlaysIn(number1);
        game.XPlaysIn(number2);
        game.XPlaysIn(number3);

        game.Check(number1).Should().Be(CellContent.X);
        game.Check(number2).Should().Be(CellContent.X);
        game.Check(number3).Should().Be(CellContent.X);
        game.GameResult().gameHasFinished.Should().BeTrue();
        game.GameResult().winner.Should().Be(CellContent.X);
    }

    [TestCase(CellNumber.One1, CellNumber.Two2, CellNumber.Three3)]
    [TestCase(CellNumber.Four4, CellNumber.Five5, CellNumber.Six6)]
    [TestCase(CellNumber.Seven7, CellNumber.Eight8, CellNumber.Nine9)]
    public void O_player_wins_horizontally(CellNumber number1, CellNumber number2, CellNumber number3) {
        var game = new TicTacToeGame();

        game.OPlaysIn(number1);
        game.OPlaysIn(number2);
        game.OPlaysIn(number3);

        game.Check(number1).Should().Be(CellContent.O);
        game.Check(number2).Should().Be(CellContent.O);
        game.Check(number3).Should().Be(CellContent.O);
        game.GameResult().gameHasFinished.Should().BeTrue();
        game.GameResult().winner.Should().Be(CellContent.O);
    }

    [TestCase(CellNumber.One1, CellNumber.Four4, CellNumber.Seven7)]
    [TestCase(CellNumber.Two2, CellNumber.Five5, CellNumber.Eight8)]
    [TestCase(CellNumber.Three3, CellNumber.Six6, CellNumber.Nine9)]
    public void x_player_wins_vertically(CellNumber number1, CellNumber number2, CellNumber number3) {
        var game = new TicTacToeGame();

        game.XPlaysIn(number1);
        game.XPlaysIn(number2);
        game.XPlaysIn(number3);

        game.Check(number1).Should().Be(CellContent.X);
        game.Check(number2).Should().Be(CellContent.X);
        game.Check(number3).Should().Be(CellContent.X);
        game.GameResult().gameHasFinished.Should().BeTrue();
        game.GameResult().winner.Should().Be(CellContent.X);
    }

    [TestCase(CellNumber.One1, CellNumber.Four4, CellNumber.Seven7)]
    [TestCase(CellNumber.Two2, CellNumber.Five5, CellNumber.Eight8)]
    [TestCase(CellNumber.Three3, CellNumber.Six6, CellNumber.Nine9)]
    public void O_player_wins_vertically(CellNumber number1, CellNumber number2, CellNumber number3) {
        var game = new TicTacToeGame();

        game.OPlaysIn(number1);
        game.OPlaysIn(number2);
        game.OPlaysIn(number3);

        game.Check(number1).Should().Be(CellContent.O);
        game.Check(number2).Should().Be(CellContent.O);
        game.Check(number3).Should().Be(CellContent.O);
        game.GameResult().gameHasFinished.Should().BeTrue();
        game.GameResult().winner.Should().Be(CellContent.O);
    }

    [TestCase(CellNumber.One1, CellNumber.Five5, CellNumber.Nine9)]
    [TestCase(CellNumber.Three3, CellNumber.Five5, CellNumber.Seven7)]
    public void x_player_wins_in_diagonal(CellNumber number1, CellNumber number2, CellNumber number3) {
        var game = new TicTacToeGame();

        game.XPlaysIn(number1);
        game.XPlaysIn(number2);
        game.XPlaysIn(number3);

        game.Check(number1).Should().Be(CellContent.X);
        game.Check(number2).Should().Be(CellContent.X);
        game.Check(number3).Should().Be(CellContent.X);
        game.GameResult().gameHasFinished.Should().BeTrue();
        game.GameResult().winner.Should().Be(CellContent.X);
    }

    [TestCase(CellNumber.One1, CellNumber.Five5, CellNumber.Nine9)]
    [TestCase(CellNumber.Three3, CellNumber.Five5, CellNumber.Seven7)]
    public void O_player_wins_in_diagonal(CellNumber number1, CellNumber number2, CellNumber number3) {
        var game = new TicTacToeGame();

        game.OPlaysIn(number1);
        game.OPlaysIn(number2);
        game.OPlaysIn(number3);

        game.Check(number1).Should().Be(CellContent.O);
        game.Check(number2).Should().Be(CellContent.O);
        game.Check(number3).Should().Be(CellContent.O);
        game.GameResult().gameHasFinished.Should().BeTrue();
        game.GameResult().winner.Should().Be(CellContent.O);
    }

    [Test]
    public void game_ends_in_draw_if_all_cells_are_full_and_there_is_no_winner() {
        var game = new TicTacToeGame();

        game.XPlaysIn(CellNumber.One1);
        game.XPlaysIn(CellNumber.Five5);
        game.XPlaysIn(CellNumber.Seven7);
        game.XPlaysIn(CellNumber.Six6);
        game.XPlaysIn(CellNumber.Two2);
        game.OPlaysIn(CellNumber.Three3);
        game.OPlaysIn(CellNumber.Four4);
        game.OPlaysIn(CellNumber.Eight8);
        game.OPlaysIn(CellNumber.Nine9);

        game.GameResult().gameHasFinished.Should().BeTrue();
        game.GameResult().isDraw.Should().BeTrue();
        game.GameResult().winner.Should().Be(CellContent.Empty);
    }
}