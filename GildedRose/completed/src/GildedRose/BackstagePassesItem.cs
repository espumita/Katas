namespace GildedRose;

public class BackstagePassesItem : GildedRoseItem {
    public BackstagePassesItem(Item item) : base(item) { }

    public override void UpdateQuality() {
        if (!HasExpired()) {
            IncreaseQuality();
            if (IsSellInEqualOrLessThanTen()) {
                IncreaseQuality();
            }
            if (IsSellInEqualOrLessThanFive()) {
                IncreaseQuality();
            }
        } else {
            SetQualityToZero();
        }
        DecreaseSellIn();
    }

    private bool IsSellInEqualOrLessThanTen() {
        return Item.SellIn <= 10;
    }

    private bool IsSellInEqualOrLessThanFive() {
        return Item.SellIn <= 5;
    }

    private void SetQualityToZero() {
        Item.Quality = 0;
    }
}