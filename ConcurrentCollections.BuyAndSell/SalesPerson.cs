namespace ConcurrentCollections.BuyAndSell;

public class SalesPerson
{
    public string Name { get; }

    public SalesPerson(string name)
    {
        Name = name;
    }

    public void Work(TimeSpan workDay, StockController controller, LogTradesQueue tradesQueue)
    {
        DateTime start = DateTime.Now;

        while (DateTime.Now - start < workDay)
        {
            string? msg = ServeCustomer(controller, tradesQueue);
            if (msg is not null)
                WriteLine($"{Name}: {msg}");
        }
    }

    public string? ServeCustomer(StockController controller, LogTradesQueue tradesQueue)
    {
        Thread.Sleep(Rnd.NextInt(3));
        TShirt shirt = TShirtProvider.SelectRandomShirt();
        string code = shirt.Code;

        bool custSells = Rnd.TrueWithProbability(1.0 / 6.0);

        if (custSells)
        {
            int quantity = Rnd.NextInt(9) + 1;
            controller.BuyShirts(code, quantity);

            tradesQueue.QueueTradeForLogging(new Trade(this, shirt, quantity, TradeType.Purchase));

            return $"Bought {quantity} of {shirt.Name}";
        }

        bool success = controller.TrySellShirt(code);
        if (success)
        {
            tradesQueue.QueueTradeForLogging(new(this, shirt, 1, TradeType.Sale));
            return $"Sold {shirt.Name}";
        }

        return $"Couldn't sell {shirt}: Out of stock";
    }
}