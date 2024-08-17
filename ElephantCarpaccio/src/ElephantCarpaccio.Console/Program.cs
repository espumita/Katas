
using ElephantCarpaccio;

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
var cashRegister = new CashRegister(statesTaxRates, discountRates);

var anItem = new Item("Cheese");
var anotherItem = new Item("Table");
var anotherOneItem = new Item("Pencil");
var aPrice = new DollarMoney(45000);
var anotherPrice = new DollarMoney(30273);
var anotherOnePrice = new DollarMoney(56066);
cashRegister.SetState(State.CA);

cashRegister.Register(anItem, aPrice, 2);
cashRegister.Register(anotherItem, anotherPrice, 3);
cashRegister.Register(anotherOneItem, anotherOnePrice, 10);

cashRegister.PrintBill().ToList().ForEach(Console.WriteLine);
