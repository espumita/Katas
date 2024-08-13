namespace GildedRose;

public class GildedRose {
    private readonly List<GildedRoseItem> Items;

    public GildedRose(List<GildedRoseItem> Items) {
        this.Items = Items;
    }

    public void UpdateQuality() {
        Items.ForEach(item => item.UpdateQuality());
    }
}