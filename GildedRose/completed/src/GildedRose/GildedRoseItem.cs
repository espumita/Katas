namespace GildedRose;

public class GildedRoseItem {
    public Item Item { get; private set; }

    public GildedRoseItem(Item item) {
        Item = item;
    }

    public virtual void UpdateQuality() {
        DecreaseQuality();
        DecreaseSellAnQuality();
    }

    protected void IncreaseQuality() {
        if (IsQualityLessThanFifty())
            Item.Quality = Item.Quality + 1;
    }

    private bool IsQualityLessThanFifty() {
        return Item.Quality < 50;
    }

    private void DecreaseQuality() {
        if (IsQualityMoreThanZero())
            Item.Quality = Item.Quality - 1;
    }

    private bool IsQualityMoreThanZero() {
        return Item.Quality > 0;
    }

    private void DecreaseSellAnQuality() {
        DecreaseSellIn();

        if (HasExpired()) {
            DecreaseQuality();
        }
    }

    protected void DecreaseSellIn() {
        Item.SellIn = Item.SellIn - 1;
    }

    protected bool HasExpired() {
        return Item.SellIn <= 0;
    }
}