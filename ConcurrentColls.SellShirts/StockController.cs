using System.Collections.Concurrent;

namespace ConcurrentCollections.SellShirts;

public enum SelectResult
{
    Success,
    NoStockLeft,
    ChosenShirtSold
}

public class StockController
{
    private ConcurrentDictionary<string, TShirt> _stock;
    public StockController(IEnumerable<TShirt> shirts)
    {
        _stock = new ConcurrentDictionary<string, TShirt>(shirts.ToDictionary(s => s.Code));
    }

    public bool Sell(string code)
    {
        return _stock.TryRemove(code, out TShirt? shirtRemoved);
    }

    public (SelectResult Result, TShirt? Shirt) SelectRandomShirt()
    {
        var keys = _stock.Keys.ToList();
        if (keys.Count == 0)
            return (SelectResult.NoStockLeft, null);

        Thread.Sleep(Rnd.NextInt(10));
        string selectedCode = keys[Rnd.NextInt(keys.Count)];
        bool found = _stock.TryGetValue(selectedCode, out TShirt? shirt);
        if (found)
            return (SelectResult.Success, shirt);

        return (SelectResult.ChosenShirtSold, null);
        //return _stock[selectedCode];
    }

    public void DisplayStock()
    {
        WriteLine($"\r\n{_stock.Count} items left in stock:");

        foreach (var shirt in _stock.Values)
        {
            WriteLine(shirt);
        }
    }
}