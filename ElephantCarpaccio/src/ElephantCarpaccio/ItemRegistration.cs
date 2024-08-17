namespace ElephantCarpaccio;

public record ItemRegistration(Item item, Money price, int quantity) {
    public Money Total() {
        return price * quantity;
    }
}