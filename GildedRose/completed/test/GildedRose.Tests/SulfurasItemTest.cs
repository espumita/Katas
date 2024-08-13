using FluentAssertions;

namespace GildedRose.Tests;

public class SulfurasItemTest {
    private const int SellIn = 0;
    private const int Quality = 80;
    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";

    [Test]
    public void when_we_update_the_day_quality_item_should_be_decreased() {
        var sulfurasItem = new SulfurasItem(new Item {
            Name = Sulfuras,
            SellIn = SellIn,
            Quality = Quality
        });
        var gildedRose = new GildedRose(new List<GildedRoseItem>{ sulfurasItem });

        gildedRose.UpdateQuality();

        sulfurasItem.Item.Quality.Should().Be(80);
        sulfurasItem.Item.SellIn.Should().Be(0);
    }
}