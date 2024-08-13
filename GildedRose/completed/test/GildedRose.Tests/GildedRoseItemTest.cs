using FluentAssertions;

namespace GildedRose.Tests;

public class GildedRoseItemTest {
    private const string Mongoose = "Elixir of the Mongoose";

    [Test]
    public void when_we_update_the_day_quality_item_should_be_decreased() {
        var gildedRoseItem = new GildedRoseItem(new Item {
            Name = Mongoose,
            SellIn = 5,
            Quality = 7
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem> { gildedRoseItem });

        gildedRose.UpdateQuality();

        gildedRoseItem.Item.Quality.Should().Be(6);
        gildedRoseItem.Item.SellIn.Should().Be(4);
    }

    [Test]
    public void when_we_update_the_day_quality_cannot_be_negative() {
        var gildedRoseItem = new GildedRoseItem(new Item {
            Name = Mongoose,
            SellIn = 0,
            Quality = 0
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem> { gildedRoseItem });

        gildedRose.UpdateQuality();

        gildedRoseItem.Item.Quality.Should().Be(0);
        gildedRoseItem.Item.SellIn.Should().Be(-1);
    }

    [Test]
    public void when_whe_update_the_day_and_there_is_no_more_days_left_quality_should_be_decreased_double() {
        var gildedRoseItem = new GildedRoseItem(new Item {
            Name = Mongoose,
            SellIn = 0,
            Quality = 8
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem> { gildedRoseItem });

        gildedRose.UpdateQuality();

        gildedRoseItem.Item.Quality.Should().Be(6);
        gildedRoseItem.Item.SellIn.Should().Be(-1);
    }
}