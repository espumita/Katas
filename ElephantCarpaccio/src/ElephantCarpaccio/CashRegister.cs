namespace ElephantCarpaccio;

public class CashRegister {
    private readonly IDictionary<State, decimal> statesTaxRates;
    private readonly IDictionary<int, decimal> discountRates;
    private State currentState;
    private List<ItemRegistration> registeredItems;
    const int LineLenght = 53;

    public CashRegister(IDictionary<State, decimal> statesTaxRates, IDictionary<int, decimal> discountRates) {
        this.statesTaxRates = statesTaxRates;
        this.discountRates = discountRates;
        registeredItems = new List<ItemRegistration>();
    }

    public void SetState(State state) {
        currentState = state;
    }

    public void Register(Item item, Money price, int quantity) {
        registeredItems.Add(new ItemRegistration(item, price, quantity));
    }

    public IEnumerable<string> PrintBill() {
        var bill = new List<string>();

        var itemLines = registeredItems.Select(ItemLines);
        bill.AddRange(itemLines);

        bill.Add(new string('-', LineLenght));
        
        var totalWithoutTaxes = TotalWithoutTaxes();
        bill.Add(LabelLine("Total without taxes", totalWithoutTaxes.ToStringWithCurrencySymbol()));
        
        var discount = Discount(totalWithoutTaxes);
        var discountValue = DiscountAmount(discount, totalWithoutTaxes);
        bill.Add(LabelLine($"Discount {DiscountPercentage(discount)}%", $"-{discountValue.ToStringWithCurrencySymbol()}"));
        
        var totalTaxes = TotalTaxes(totalWithoutTaxes - discountValue);
        bill.Add(LabelLine($"Tax {TaxPercentage()}%", $"+{totalTaxes.ToStringWithCurrencySymbol()}"));
        
        bill.Add(new string('-', LineLenght));
        
        var totalPrice = (totalWithoutTaxes - discountValue + totalTaxes).ToStringWithCurrencySymbol();
        bill.Add(LabelLine("Total price", $"{totalPrice}"));
        return bill;
    }

    private static string ItemLines(ItemRegistration registration) {
        var itemLabel = FormatLeftItemLabel(registration.item.label, 20);
        var quantityLabel = FormatLeftItemLabel(registration.quantity.ToString(), (LineLenght - 20) / 3);
        var unitPriceLabel = FormatLeftItemLabel(registration.price.ToStringWithCurrencySymbol(), (LineLenght - 20) / 3);
        var totalPriceLabel = FormatRightItemLabel(registration.Total().ToStringWithCurrencySymbol(), (LineLenght - 20) / 3);
        return $"{itemLabel}{quantityLabel}{unitPriceLabel}{totalPriceLabel}";
    }

    private static string FormatLeftItemLabel(string label, int maxSize) {
        return $"{label}{new string(' ', maxSize - label.Length)}";
    }

    private static string FormatRightItemLabel(string label, int maxSize) {
        return $"{new string(' ', maxSize - label.Length)}{label}";
    }

    private static string LabelLine(string label, string value) {
        return $"{label}{new string(' ', LineLenght - label.Length - value.Length)}{value}";
    }

    private Money TotalWithoutTaxes() {
        Money total = new DollarMoney(0);
        registeredItems.ForEach(registration => {
            total += registration.Total();
        });
        return total;
    }

    private string TaxPercentage() {
        return $"{statesTaxRates[currentState]:0.00}";
    }

    private string DiscountPercentage(decimal value) {
        return $"{value:0.00}";
    }

    private Money TotalTaxes(Money totalWithoutTaxes) {
        return totalWithoutTaxes * (statesTaxRates[currentState] / 100);
    }

    private decimal Discount(Money value) {
        var discount = 0.0m;
        foreach (var discountRatesKey in discountRates.Keys) {
            if (discountRatesKey > value.Value) break;
            discount = discountRates[discountRatesKey];
        }
        return discount;
    }

    private Money DiscountAmount(decimal discount, Money value) {
        return discount == 0.0m
            ? new DollarMoney(0)
            : value * (discount / 100);
    }

}

