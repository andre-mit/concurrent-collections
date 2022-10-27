using System.Collections.Concurrent;

namespace ConcurrentCollections.BuyAndSell;

public class StaffRecords
{
    private ConcurrentDictionary<SalesPerson, int> _commissions = new();

    public void LogTrade(Trade trade)
    {
        Thread.Sleep(30);
        if (trade.IsSale)
        {
            int tradeValue = trade.Shirt.PricePence * trade.Quantity;
            int commission = tradeValue / 100;
            _commissions.AddOrUpdate(trade.Person, commission, (_, oldValue) => oldValue + commission);
        }
    }

    public void DisplayCommissions(IEnumerable<SalesPerson> people)
    {
        WriteLine("\nBonus by salesperson:");

        foreach (var person in people)
        {
            int bonus = _commissions.GetOrAdd(person, 0);
            WriteLine($"{person.Name,15} earned ${bonus / 100}.{bonus % 100:00}");
        }
    }
}