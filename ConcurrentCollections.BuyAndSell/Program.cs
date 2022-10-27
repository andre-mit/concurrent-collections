using ConcurrentCollections.BuyAndSell;

StockController stockController = new StockController();
TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);

StaffRecords staffLogs = new();
LogTradesQueue tradesQueue = new(staffLogs);

SalesPerson[] staff =
{
    new("Andre"),
    new("Pedro"),
    new("Jose"),
    new("Carlos")
};

List<Task> salesTasks = staff.Select(person => Task.Run(() => person.Work(workDay, stockController, tradesQueue))).ToList();

Task[] loggingTasks =
{
    Task.Run(() => tradesQueue.MonitorAndLogTrades()),
    Task.Run(() => tradesQueue.MonitorAndLogTrades())
};

Task.WaitAll(salesTasks.ToArray());
tradesQueue.SetNoMoreTrades();
Task.WaitAll(loggingTasks);

stockController.DisplayStock();
staffLogs.DisplayCommissions(staff);