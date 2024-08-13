using FluentAssertions;

namespace GildedRose.Tests;

public class AgedBrieItemTest {
    private const int SellIn = 2;
    private const int Quality = 0;
    private const string AgedBrie = "Aged Brie";

    [Test]
    public void when_whe_update_the_day_quality_should_be_increased() {
        var agedBrieItem = new AgedBrieItem(new Item {
            Name = AgedBrie,
            SellIn = SellIn,
            Quality = Quality
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem> { agedBrieItem });

        gildedRose.UpdateQuality();

        agedBrieItem.Item.Quality.Should().Be(1);
        agedBrieItem.Item.SellIn.Should().Be(1);
    }

    [Test]
    public void when_we_update_the_day_quality_cannot_be_more_than_fifty() {
        var agedBrieItem = new AgedBrieItem(new Item {
            Name = AgedBrie,
            SellIn = 3,
            Quality = 50
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem> { agedBrieItem });

        gildedRose.UpdateQuality();

        agedBrieItem.Item.Quality.Should().Be(50);
        agedBrieItem.Item.SellIn.Should().Be(2);

    }

}