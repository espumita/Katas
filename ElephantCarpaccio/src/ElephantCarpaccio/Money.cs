namespace ElephantCarpaccio;

public record Money(int Value, int NumberOfDecimals, string Iso4217CurrencyCode, char Symbol) {

    public static Money operator +(Money a, Money b) {
        var newValue = a.Value + b.Value;
        return new Money(newValue, a.NumberOfDecimals, a.Iso4217CurrencyCode, a.Symbol);
    }

    public static Money operator -(Money a, Money b) {
        var newValue = a.Value - b.Value;
        return new Money(newValue, a.NumberOfDecimals, a.Iso4217CurrencyCode, a.Symbol);
    }

    public static Money operator *(Money a, decimal number) {
        var newValue = (int)(a.Value * number);
        return new Money(newValue, a.NumberOfDecimals, a.Iso4217CurrencyCode, a.Symbol);
    }

    public string ToStringWithCurrencySymbol() {
        var rawValue = Value.ToString();
        if (rawValue.Length < NumberOfDecimals) return "$0.00";
        var number = rawValue.Substring(0, rawValue.Length - NumberOfDecimals);
        var decimals = rawValue.Substring(rawValue.Length - NumberOfDecimals);
        return $"${number}.{decimals}";
    }
};