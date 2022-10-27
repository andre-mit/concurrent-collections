namespace ConcurrentCollections.BuyAndSell;

public class LogTradesQueue
{
    // can use IProducerConsumerCollection<T> instead of any of the concrete classes below (ConcurrentQueue<T>, ConcurrentStack<T>, ConcurrentBag<T>)
    private BlockingCollection<Trade> _tradesToLog = new(new ConcurrentQueue<Trade>());
    private readonly StaffRecords _staffRecords;

    public LogTradesQueue(StaffRecords staffRecords)
    {
        _staffRecords = staffRecords;
    }

    public void SetNoMoreTrades() => _tradesToLog.CompleteAdding();
    public void QueueTradeForLogging(Trade trade) => _tradesToLog.TryAdd(trade);

    public void MonitorAndLogTrades()
    {
        foreach (Trade nextTrade in _tradesToLog.GetConsumingEnumerable())
        {
            _staffRecords.LogTrade(nextTrade);
            WriteLine("Processing transaction from {0}", nextTrade.Person.Name);
        }
    }
}