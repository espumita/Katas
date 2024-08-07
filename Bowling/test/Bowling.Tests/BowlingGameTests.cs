using FluentAssertions;

namespace Bowling.Tests;

public class BowlingGameTests {

    [Test]
    public void start_the_game_with_empty_score() {
        var player1 = new Player("Player1");
        
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Scores.Keys.Single().Should().Be(player1);
        var player1Score = bowling.Scores[player1];
        player1Score.Score.Should().Be(0);
    }

    [Test]
    public void roll_some_pins() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 3);
        bowling.Roll(player1, 6);

        bowling.Scores[player1].Score.Should().Be(9);
    }

    [Test]
    public void roll_no_pins() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 0);
        bowling.Roll(player1, 0);

        bowling.Scores[player1].Score.Should().Be(0);
    }

    [Test]
    public void roll_only_1_pin_on_the_second_roll() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 0);
        bowling.Roll(player1, 1);

        bowling.Scores[player1].Score.Should().Be(1);
    }

    [Test]
    public void roll_only_1_pin_on_the_first_roll() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 1);
        bowling.Roll(player1, 0);

        bowling.Scores[player1].Score.Should().Be(1);
    }

    [Test]
    public void roll_spare_but_no_pins_in_next_roll() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 3);
        bowling.Roll(player1, 7);
        bowling.Roll(player1, 0);

        bowling.Scores[player1].Score.Should().Be(10);
    }

    [Test]
    public void roll_spare_but_with_some_pins_in_next_roll() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 3);
        bowling.Roll(player1, 7);
        bowling.Roll(player1, 2);

        bowling.Scores[player1].Score.Should().Be(14);
    }

    [Test]
    public void roll_strike_but_no_pins_in_next_roll() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 0);
        bowling.Roll(player1, 0);

        bowling.Scores[player1].Score.Should().Be(10);
    }

    [Test]
    public void roll_strike_with_1_pin_in_next_roll() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 1);
        bowling.Roll(player1, 0);

        bowling.Scores[player1].Score.Should().Be(12);
    }

    [Test]
    public void roll_strike_with_1_pin_in_next_one_roll() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 0);
        bowling.Roll(player1, 1);

        bowling.Scores[player1].Score.Should().Be(12);
    }

    [Test]
    public void roll_strike_with_some_pins_in_next_one_roll() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 7);
        bowling.Roll(player1, 2);

        bowling.Scores[player1].Score.Should().Be(28);
    }

    [Test]
    public void roll_10_pins_but_is_not_an_strike() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 0);
        bowling.Roll(player1, 10);
        bowling.Roll(player1, 1);
        bowling.Roll(player1, 1);

        bowling.Scores[player1].Score.Should().Be(13);
    }

    [Test]
    public void roll_strike_but_only_next_spin() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 1);
        bowling.Roll(player1, 1);
        bowling.Roll(player1, 1);

        bowling.Scores[player1].Score.Should().Be(15);
    }

    [Test]
    public void roll_double_strike() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);

        bowling.Scores[player1].Score.Should().Be(30);
    }

    [Test]
    public void roll_triple_strike() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);

        bowling.Scores[player1].Score.Should().Be(60);
    }

    [Test]
    public void roll_full_game_without_pins() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        foreach (var i in Enumerable.Range(1, 9)) {
            bowling.Roll(player1, 0);
            bowling.Roll(player1, 0);
        }
        bowling.Roll(player1, 0);
        bowling.Roll(player1, 0);

        bowling.Scores[player1].Score.Should().Be(0);
    }

    [Test]
    public void roll_full_game_but_spare_in_last_frame() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        foreach (var i in Enumerable.Range(1, 9)) {
            bowling.Roll(player1, 0);
            bowling.Roll(player1, 0);
        }
        bowling.Roll(player1, 3);
        bowling.Roll(player1, 7);
        bowling.Roll(player1, 1);

        bowling.Scores[player1].Score.Should().Be(11);
    }

    [Test]
    public void roll_full_game_but_strike_in_last_frame() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        foreach (var i in Enumerable.Range(1, 9)) {
            bowling.Roll(player1, 0);
            bowling.Roll(player1, 0);
        }
        bowling.Roll(player1, 10);
        bowling.Roll(player1, 0);
        bowling.Roll(player1, 0);

        bowling.Scores[player1].Score.Should().Be(10);
    }

    [Test]
    public void roll_full_game_but_double_strike_in_last_frame() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        foreach (var i in Enumerable.Range(1, 9)) {
            bowling.Roll(player1, 0);
            bowling.Roll(player1, 0);
        }
        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);
        bowling.Roll(player1, 0);

        bowling.Scores[player1].Score.Should().Be(20);
    }

    [Test]
    public void roll_max_points() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        foreach (var i in Enumerable.Range(1, 9)) {
            bowling.Roll(player1, 10);
        }

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);

        bowling.Scores[player1].Score.Should().Be(300);
    }

    [Test]
    public void complex_game_with_several_different_rolls() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);
        
        bowling.Roll(player1, 7);
        bowling.Roll(player1, 2);
        bowling.Roll(player1, 8);
        bowling.Roll(player1, 2);
        bowling.Roll(player1, 0);
        bowling.Roll(player1, 9);
        
        bowling.Roll(player1, 10);
        bowling.Roll(player1, 7);
        bowling.Roll(player1, 3);
        bowling.Roll(player1, 9);
        bowling.Roll(player1, 0);

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);
        bowling.Roll(player1, 8);

        bowling.Scores[player1].Score.Should().Be(180);
    }

    [Test]
    public void game_not_ends_while_players_has_pending_rolls() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        foreach (var i in Enumerable.Range(1, 9)) {
            bowling.Roll(player1, 10);
        }

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);

        var playerScore = bowling.Scores[player1];
        playerScore.Score.Should().Be(290);
        playerScore.PlayerEnds().Should().BeFalse();
        bowling.GameEnds().Should().BeFalse();
    }

    [Test]
    public void game_ends_when_players_has_no_pending_rolls() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        foreach (var i in Enumerable.Range(1, 9)) {
            bowling.Roll(player1, 10);
        }

        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);
        bowling.Roll(player1, 10);

        var playerScore = bowling.Scores[player1];
        playerScore.Score.Should().Be(300);
        playerScore.PlayerEnds().Should().BeTrue();
        bowling.GameEnds().Should().BeTrue();
    }

    [Test]
    public void game_ends_when_players_has_no_pending_rolls_and_there_is_no_strike() {
        var player1 = new Player("Player1");
        var bowling = new BowlingGame(new List<Player> { player1 });

        foreach (var i in Enumerable.Range(1, 9)) {
            bowling.Roll(player1, 10);
        }

        bowling.Roll(player1, 2);
        bowling.Roll(player1, 3);

        var playerScore = bowling.Scores[player1];
        playerScore.Score.Should().Be(252);
        playerScore.PlayerEnds().Should().BeTrue();
        bowling.GameEnds().Should().BeTrue();
    }

}