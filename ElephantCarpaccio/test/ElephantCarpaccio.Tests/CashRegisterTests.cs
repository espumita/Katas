using FluentAssertions;

namespace ElephantCarpaccio.Tests;

public class CashRegisterTests {
    private CashRegister cashRegister;


    [SetUp]
    public void SetUp() {
        var statesTaxRates = new Dictionary<State, decimal> {
            {State.UT, 0.685m },
            {State.NV, 8.000m },
            {State.TX, 6.250m },
            {State.AL, 4.000m },
            {State.CA, 8.250m }
        };
        var discountRates = new Dictionary<int, decimal> {
            {100000, 3.000m },
            {500000, 5.000m },
            {700000, 7.000m },
            {1000000, 10.000m },
            {5000000, 15.000m }
        };
        cashRegister = new CashRegister(statesTaxRates, discountRates);
    }

    [Test]
    public void print_recipe_for_one_item() {
        var anItem = new Item("Cheese");
        var aPrice = new DollarMoney(10000);
        cashRegister.SetState(State.CA);

        cashRegister.Register(anItem, aPrice,1);

        var billLines = cashRegister.PrintBill().ToList();

        billLines.Count.Should().Be(7);
        billLines[0].Should().Be("Cheese              1          $100.00        $100.00");
        billLines[1].Should().Be("-----------------------------------------------------");
        billLines[2].Should().Be("Total without taxes                           $100.00");
        billLines[3].Should().Be("Discount 0.00%                                 -$0.00");
        billLines[4].Should().Be("Tax 8.25%                                      +$8.25");
        billLines[5].Should().Be("-----------------------------------------------------");
        billLines[6].Should().Be("Total price                                   $108.25");
    }

    [Test]
    public void print_recipe_for_multiple_items() {
        var anItem = new Item("Cheese");
        var anotherItem = new Item("Table");
        var aPrice = new DollarMoney(10000);
        cashRegister.SetState(State.CA);

        cashRegister.Register(anItem, aPrice, 1);
        cashRegister.Register(anotherItem, aPrice, 1);

        var billLines = cashRegister.PrintBill().ToList();

        billLines.Count.Should().Be(8);
        billLines[0].Should().Be("Cheese              1          $100.00        $100.00");
        billLines[1].Should().Be("Table               1          $100.00        $100.00");
        billLines[2].Should().Be("-----------------------------------------------------");
        billLines[3].Should().Be("Total without taxes                           $200.00");
        billLines[4].Should().Be("Discount 0.00%                                 -$0.00");
        billLines[5].Should().Be("Tax 8.25%                                     +$16.50");
        billLines[6].Should().Be("-----------------------------------------------------");
        billLines[7].Should().Be("Total price                                   $216.50");
    }

    [Test]
    public void print_recipe_for_multiple_items_with_multiple_quantities() {
        var anItem = new Item("Cheese");
        var anotherItem = new Item("Table");
        var aPrice = new DollarMoney(10000);
        cashRegister.SetState(State.CA);

        cashRegister.Register(anItem, aPrice, 2);
        cashRegister.Register(anotherItem, aPrice, 3);

        var billLines = cashRegister.PrintBill().ToList();

        billLines.Count.Should().Be(8);
        billLines[0].Should().Be("Cheese              2          $100.00        $200.00");
        billLines[1].Should().Be("Table               3          $100.00        $300.00");
        billLines[2].Should().Be("-----------------------------------------------------");
        billLines[3].Should().Be("Total without taxes                           $500.00");
        billLines[4].Should().Be("Discount 0.00%                                 -$0.00");
        billLines[5].Should().Be("Tax 8.25%                                     +$41.25");
        billLines[6].Should().Be("-----------------------------------------------------");
        billLines[7].Should().Be("Total price                                   $541.25");
    }

    [Test]
    public void print_recipe_for_a_different_state() {
        var anItem = new Item("Cheese");
        var aPrice = new DollarMoney(10000);
        cashRegister.SetState(State.AL);

        cashRegister.Register(anItem, aPrice, 1);

        var billLines = cashRegister.PrintBill().ToList();

        billLines.Count.Should().Be(7);
        billLines[0].Should().Be("Cheese              1          $100.00        $100.00");
        billLines[1].Should().Be("-----------------------------------------------------");
        billLines[2].Should().Be("Total without taxes                           $100.00");
        billLines[3].Should().Be("Discount 0.00%                                 -$0.00");
        billLines[4].Should().Be("Tax 4.00%                                      +$4.00");
        billLines[5].Should().Be("-----------------------------------------------------");
        billLines[6].Should().Be("Total price                                   $104.00");
    }

    [TestCase]
    public void print_recipe_for_a_discount() {
        var anItem = new Item("Cheese");
        var aPrice = new DollarMoney(100000);
        cashRegister.SetState(State.CA);

        cashRegister.Register(anItem, aPrice, 1);

        var billLines = cashRegister.PrintBill().ToList();

        billLines.Count.Should().Be(7);
        billLines[0].Should().Be("Cheese              1          $1000.00      $1000.00");
        billLines[1].Should().Be("-----------------------------------------------------");
        billLines[2].Should().Be("Total without taxes                          $1000.00");
        billLines[3].Should().Be("Discount 3.00%                                -$30.00");
        billLines[4].Should().Be("Tax 8.25%                                     +$80.02");
        billLines[5].Should().Be("-----------------------------------------------------");
        billLines[6].Should().Be("Total price                                  $1050.02");
    }
}