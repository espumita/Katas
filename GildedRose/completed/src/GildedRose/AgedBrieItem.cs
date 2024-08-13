namespace GildedRose;

public class AgedBrieItem : GildedRoseItem{
    public AgedBrieItem(Item item) : base(item) { }
    public override void UpdateQuality() {
        IncreaseQuality();
        DecreaseSellIn();
    }
}