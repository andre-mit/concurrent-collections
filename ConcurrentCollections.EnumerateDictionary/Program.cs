using System.Collections.Concurrent;

var stock = new ConcurrentDictionary<string, int>();
stock.TryAdd("apple", 0);
stock.TryAdd("orange", 0);
stock.TryAdd("banana", 0);


foreach (var product in stock.ToArray())
{
    stock["apple"] += 1;
    Console.WriteLine("{0}: {1}", product.Key, product.Value);
}