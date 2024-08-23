using FluentAssertions;

namespace PokerHands.Tests;

public class PokerGameTests {

    [Test]
    public void pairs_wins_over_high_card() {
        var game = new PokerGame();

        game.AddPlayerHand("Player1", new List<Card> {
            new Card(CardValue.Two2, Suit.Hearts),
            new Card(CardValue.Two2, Suit.Diamonds),
            new Card(CardValue.Five5, Suit.Spades),
            new Card(CardValue.Nine9, Suit.Clubs),
            new Card(CardValue.King, Suit.Diamonds)
        });

        game.AddPlayerHand("Player2", new List<Card> {
            new Card(CardValue.Two2, Suit.Clubs),
            new Card(CardValue.Tree3, Suit.Hearts),
            new Card(CardValue.Four4, Suit.Spades),
            new Card(CardValue.Eight8, Suit.Clubs),
            new Card(CardValue.Ace, Suit.Hearts)
        });

        var comparisonResult = game.CompareHands();

        comparisonResult.winnerPlayerName.Should().Be("Player1");
        comparisonResult.handRank.Should().Be(HandRank.Pair);
    }

    [Test]
    public void two_pairs_wins_over_single_pair() {
        var game = new PokerGame();

        game.AddPlayerHand("Player1", new List<Card> {
            new Card(CardValue.Two2, Suit.Hearts),
            new Card(CardValue.Two2, Suit.Diamonds),
            new Card(CardValue.Five5, Suit.Spades),
            new Card(CardValue.Nine9, Suit.Clubs),
            new Card(CardValue.King, Suit.Diamonds)
        });

        game.AddPlayerHand("Player2", new List<Card> {
            new Card(CardValue.Two2, Suit.Clubs),
            new Card(CardValue.Two2, Suit.Spades),
            new Card(CardValue.Tree3, Suit.Spades),
            new Card(CardValue.Tree3, Suit.Clubs),
            new Card(CardValue.Ace, Suit.Hearts)
        });

        var comparisonResult = game.CompareHands();

        comparisonResult.winnerPlayerName.Should().Be("Player2");
        comparisonResult.handRank.Should().Be(HandRank.TwoPairs);
    }

    [Test]
    public void tree_of_a_kind_wins_over_two_pairs() {
        var game = new PokerGame();

        game.AddPlayerHand("Player1", new List<Card> {
            new Card(CardValue.Two2, Suit.Hearts),
            new Card(CardValue.Two2, Suit.Diamonds),
            new Card(CardValue.Two2, Suit.Spades),
            new Card(CardValue.Nine9, Suit.Clubs),
            new Card(CardValue.King, Suit.Diamonds)
        });

        game.AddPlayerHand("Player2", new List<Card> {
            new Card(CardValue.Four4, Suit.Clubs),
            new Card(CardValue.Four4, Suit.Spades),
            new Card(CardValue.Tree3, Suit.Spades),
            new Card(CardValue.Tree3, Suit.Clubs),
            new Card(CardValue.Ace, Suit.Hearts)
        });

        var comparisonResult = game.CompareHands();

        comparisonResult.winnerPlayerName.Should().Be("Player1");
        comparisonResult.handRank.Should().Be(HandRank.ThreeOfAKind);
    }

    [Test]
    public void straight_wins_over_tree_of_a_kind() {
        var game = new PokerGame();

        game.AddPlayerHand("Player1", new List<Card> {
            new Card(CardValue.Two2, Suit.Hearts),
            new Card(CardValue.Two2, Suit.Diamonds),
            new Card(CardValue.Two2, Suit.Spades),
            new Card(CardValue.Nine9, Suit.Clubs),
            new Card(CardValue.King, Suit.Diamonds)
        });

        game.AddPlayerHand("Player2", new List<Card> {
            new Card(CardValue.Four4, Suit.Clubs),
            new Card(CardValue.Five5, Suit.Spades),
            new Card(CardValue.Six6, Suit.Spades),
            new Card(CardValue.Seven7, Suit.Clubs),
            new Card(CardValue.Eight8, Suit.Hearts)
        });

        var comparisonResult = game.CompareHands();

        comparisonResult.winnerPlayerName.Should().Be("Player2");
        comparisonResult.handRank.Should().Be(HandRank.Straight);
    }

    [Test]
    public void flush_wins_over_straight() {
        var game = new PokerGame();

        game.AddPlayerHand("Player1", new List<Card> {
            new Card(CardValue.Two2, Suit.Hearts),
            new Card(CardValue.Tree3, Suit.Hearts),
            new Card(CardValue.Ace, Suit.Hearts),
            new Card(CardValue.Nine9, Suit.Hearts),
            new Card(CardValue.King, Suit.Hearts)
        });

        game.AddPlayerHand("Player2", new List<Card> {
            new Card(CardValue.Four4, Suit.Clubs),
            new Card(CardValue.Five5, Suit.Spades),
            new Card(CardValue.Six6, Suit.Spades),
            new Card(CardValue.Seven7, Suit.Clubs),
            new Card(CardValue.Eight8, Suit.Hearts)
        });

        var comparisonResult = game.CompareHands();

        comparisonResult.winnerPlayerName.Should().Be("Player1");
        comparisonResult.handRank.Should().Be(HandRank.Flush);
    }

    [Test]
    public void full_house_wins_over_flush() {
        var game = new PokerGame();

        game.AddPlayerHand("Player1", new List<Card> {
            new Card(CardValue.Two2, Suit.Hearts),
            new Card(CardValue.Tree3, Suit.Hearts),
            new Card(CardValue.Ace, Suit.Hearts),
            new Card(CardValue.Nine9, Suit.Hearts),
            new Card(CardValue.King, Suit.Hearts)
        });

        game.AddPlayerHand("Player2", new List<Card> {
            new Card(CardValue.Four4, Suit.Clubs),
            new Card(CardValue.Four4, Suit.Spades),
            new Card(CardValue.Four4, Suit.Hearts),
            new Card(CardValue.Seven7, Suit.Clubs),
            new Card(CardValue.Seven7, Suit.Hearts)
        });

        var comparisonResult = game.CompareHands();

        comparisonResult.winnerPlayerName.Should().Be("Player2");
        comparisonResult.handRank.Should().Be(HandRank.FullHouse);
    }

    [Test]
    public void full_off_a_kind_wins_over_full_house() {
        var game = new PokerGame();

        game.AddPlayerHand("Player1", new List<Card> {
            new Card(CardValue.Two2, Suit.Hearts),
            new Card(CardValue.Two2, Suit.Diamonds),
            new Card(CardValue.Two2, Suit.Clubs),
            new Card(CardValue.Two2, Suit.Spades),
            new Card(CardValue.King, Suit.Hearts)
        });

        game.AddPlayerHand("Player2", new List<Card> {
            new Card(CardValue.Four4, Suit.Clubs),
            new Card(CardValue.Four4, Suit.Spades),
            new Card(CardValue.Four4, Suit.Hearts),
            new Card(CardValue.Seven7, Suit.Clubs),
            new Card(CardValue.Seven7, Suit.Hearts)
        });

        var comparisonResult = game.CompareHands();

        comparisonResult.winnerPlayerName.Should().Be("Player1");
        comparisonResult.handRank.Should().Be(HandRank.FourOfAKind);
    }

    [Test]
    public void straight_flush_over_full_off_a_kind() {
        var game = new PokerGame();

        game.AddPlayerHand("Player1", new List<Card> {
            new Card(CardValue.Two2, Suit.Hearts),
            new Card(CardValue.Two2, Suit.Diamonds),
            new Card(CardValue.Two2, Suit.Clubs),
            new Card(CardValue.Two2, Suit.Spades),
            new Card(CardValue.King, Suit.Hearts)
        });

        game.AddPlayerHand("Player2", new List<Card> {
            new Card(CardValue.Four4, Suit.Clubs),
            new Card(CardValue.Seven7, Suit.Clubs),
            new Card(CardValue.Six6, Suit.Clubs),
            new Card(CardValue.Five5, Suit.Clubs),
            new Card(CardValue.Eight8, Suit.Clubs)
        });

        var comparisonResult = game.CompareHands();

        comparisonResult.winnerPlayerName.Should().Be("Player2");
        comparisonResult.handRank.Should().Be(HandRank.StraightFlush);
    }

    [Test]
    public void royal_flush_over_full_straight_flush() {
        var game = new PokerGame();

        game.AddPlayerHand("Player1", new List<Card> {
            new Card(CardValue.Ace, Suit.Hearts),
            new Card(CardValue.King, Suit.Hearts),
            new Card(CardValue.Queen, Suit.Hearts),
            new Card(CardValue.Jack, Suit.Hearts),
            new Card(CardValue.Ten10, Suit.Hearts)
        });

        game.AddPlayerHand("Player2", new List<Card> {
            new Card(CardValue.Four4, Suit.Clubs),
            new Card(CardValue.Seven7, Suit.Clubs),
            new Card(CardValue.Six6, Suit.Clubs),
            new Card(CardValue.Five5, Suit.Clubs),
            new Card(CardValue.Eight8, Suit.Clubs)
        });

        var comparisonResult = game.CompareHands();

        comparisonResult.winnerPlayerName.Should().Be("Player1");
        comparisonResult.handRank.Should().Be(HandRank.RoyalFlush);
    }
}