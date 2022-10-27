namespace ConcurrentCollections.SellShirts;

public class SalesPerson
{
    public string Name { get; }

    public SalesPerson(string name)
    {
        Name = name;
    }

    public void Work(TimeSpan workDay, StockController controller)
    {
        DateTime start = DateTime.Now;

        while (DateTime.Now - start < workDay)
        {
            var result = ServeCustomer(controller);
            if (result.Status is not null)
                WriteLine($"{Name}: {result.Status}");

            if (!result.ShirtsInStock)
                break;
        }
    }

    public (bool ShirtsInStock, string? Status) ServeCustomer(StockController controller)
    {
        var result = controller.SelectRandomShirt();
        var shirt = result.Shirt;

        if (result.Result == SelectResult.NoStockLeft)
            return (false, "All shirts sold");

        if (result.Result == SelectResult.ChosenShirtSold)
            return (true, "Can't show shirt to customer - already sold");

        Thread.Sleep(Rnd.NextInt(30));

        // customer chooses to buy with only 20% probability
        if (Rnd.TrueWiithProbability(0.2))
        {
            var sold = controller.Sell(shirt.Code);
            if (sold) return (true, $"Sold {shirt.Name}");

            return (true, $"Can't sell {shirt.Name} - already sold");
        }

        return (true, null);
    }
}