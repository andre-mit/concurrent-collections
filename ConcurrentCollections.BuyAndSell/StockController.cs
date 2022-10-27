using System.Collections.Concurrent;

namespace ConcurrentCollections.BuyAndSell;

public class StockController
{
    private ConcurrentDictionary<string, int> _stock = new();
    private int _totalQuantityBought;
    private int _totalQuantitySold;

    public void BuyShirts(string code, int quantityToBuy)
    {
        _stock.AddOrUpdate(code, quantityToBuy, (_, quantity) => quantity + quantityToBuy);
        Interlocked.Add(ref _totalQuantityBought, quantityToBuy);
    }

    public bool TrySellShirt(string code)
    {
        bool success = false;

        int newStockLevel = _stock.AddOrUpdate(code,
            _ => { success = false; return 0; },
            (_, quantity) =>
            {
                if (quantity == 0)
                {
                    success = false;
                    return 0;
                }

                success = true;
                return quantity - 1;
            });

        if (success)
            Interlocked.Increment(ref _totalQuantitySold);

        return success;
    }

    public void DisplayStock()
    {
        WriteLine($"Stock levels by item");

        foreach (var shirt in TShirtProvider.AllShirts)
        {
            //_stock.TryGetValue(shirt.Code, out int stockLevel);
            int stockLevel = _stock.GetOrAdd(shirt.Code, 0);
            WriteLine($"{shirt.Name,-30}: {stockLevel}");
        }

        int totalStock = _stock.Values.Sum();
        WriteLine($"\r\nBought = {_totalQuantityBought,-30}");
        WriteLine($"Sold = {_totalQuantitySold,-30}");
        WriteLine($"Stock = {totalStock,-30}");

        int error = totalStock + _totalQuantitySold - _totalQuantityBought;

        WriteLine(error == 0 ? "Stock levels match" : $"Error in stockLevel: {error}");
    }
}