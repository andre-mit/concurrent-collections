using ConcurrentCollections.SellShirts;

StockController stockController = new StockController(TShirtProvider.AllShirts);
TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);

Task task1 = Task.Run(() => new SalesPerson("Andre").Work(workDay, stockController));
Task task2 = Task.Run(() => new SalesPerson("Pedro").Work(workDay, stockController));
Task task3 = Task.Run(() => new SalesPerson("Jose").Work(workDay, stockController));

Task.WaitAll(task1, task2, task3);

stockController.DisplayStock();