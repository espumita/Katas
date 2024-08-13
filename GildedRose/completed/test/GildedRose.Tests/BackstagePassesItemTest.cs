using FluentAssertions;

namespace GildedRose.Tests;

public class BackstagePassesItemTest {
    private const string BackStagePasses = "Backstage passes to a TAFKAL80ETC concert";

    [Test]
    public void when_whe_update_the_day_and_there_are_more_than_ten_days_left_quality_should_be_increased_by_one() {
        var backstagePassesItem = new BackstagePassesItem(new Item {
            Name = BackStagePasses,
            SellIn = 20,
            Quality = 4
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem> { backstagePassesItem });

        gildedRose.UpdateQuality();

        backstagePassesItem.Item.Quality.Should().Be(5);
        backstagePassesItem.Item.SellIn.Should().Be(19);
    }

    [Test]
    public void when_whe_update_the_day_and_there_are_more_than_five_days_and_less_than_ten_days_quality_should_be_increased_by_two() {
        var backstagePassesItem = new BackstagePassesItem(new Item {
            Name = BackStagePasses,
            SellIn = 10,
            Quality = 4
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem> { backstagePassesItem });

        gildedRose.UpdateQuality();

        backstagePassesItem.Item.Quality.Should().Be(6);
        backstagePassesItem.Item.SellIn.Should().Be(9);
    }

    [Test]
    public void when_whe_update_the_day_and_there_are_more_days_than_zero_less_days_than_ten_quality_should_be_increased_by_three() {
        var backstagePassesItem = new BackstagePassesItem(new Item {
            Name = BackStagePasses,
            SellIn = 5,
            Quality = 4
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem> { backstagePassesItem });

        gildedRose.UpdateQuality();

        backstagePassesItem.Item.Quality.Should().Be(7);
        backstagePassesItem.Item.SellIn.Should().Be(4);
    }

    [Test]
    public void when_whe_update_the_day_and_there_are_less_days_left_than_zero_quality_should_be_zero() {
        var backstagePassesItem = new BackstagePassesItem(new Item {
            Name = BackStagePasses,
            SellIn = 0,
            Quality = 4
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem> { backstagePassesItem });

        gildedRose.UpdateQuality();

        backstagePassesItem.Item.Quality.Should().Be(0);
        backstagePassesItem.Item.SellIn.Should().Be(-1);
    }

    [Test]
    public void when_we_update_the_day_quality_cannot_be_more_than_fifty() {
        var backstagePassesItem = new BackstagePassesItem(new Item {
            Name = BackStagePasses,
            SellIn = 3,
            Quality = 50
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem> { backstagePassesItem });

        gildedRose.UpdateQuality();

        backstagePassesItem.Item.Quality.Should().Be(50);
        backstagePassesItem.Item.SellIn.Should().Be(2);
    }
}