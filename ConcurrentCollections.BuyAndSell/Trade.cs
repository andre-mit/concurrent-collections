namespace ConcurrentCollections.BuyAndSell;

public enum TradeType { Sale, Purchase }

public class Trade
{
    public SalesPerson Person { get; private set; }
    public TShirt Shirt { get; private set; }
    public int Quantity { get; private set; }
    public TradeType Type { get; private set; }
    public bool IsSale => Type == TradeType.Sale;

    public Trade(SalesPerson person, TShirt shirt, int quantity, TradeType type)
    {
        Person = person;
        Shirt = shirt;
        Quantity = quantity;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Person.Name} {Type} {Quantity} {Shirt.Name}";
    }
}