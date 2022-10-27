using System.Collections.Concurrent;
using static System.Console;

var ordersQueue = new ConcurrentQueue<string>();
Task task1 = Task.Run(() => PlaceOrders(ordersQueue, "Andre", 5));
Task task2 = Task.Run(() => PlaceOrders(ordersQueue, "Augusto", 5));

Task.WaitAll(task1, task2);

foreach (var order in ordersQueue)
    WriteLine("Order: {0}", order);

static void PlaceOrders(ConcurrentQueue<string> orders, string customerName, int nOrders)
{
    for (int i = 1; i <= nOrders; i++)
    {
        Thread.Sleep(new TimeSpan(1));
        string orderName = $"{customerName} wants t-shirt {i}";
        orders.Enqueue(orderName);
    }
}