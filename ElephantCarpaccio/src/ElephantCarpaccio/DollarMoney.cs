namespace ElephantCarpaccio;

public record DollarMoney(int Value) : Money(Value, 2, "USD", '$');