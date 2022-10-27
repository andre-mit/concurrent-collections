namespace ConcurrentCollections.DictionaryPerformance;

public class Worker
{
    public static int DoSomething()
    {
        int total = 0;
        for (int i = 0; i < 1000; i++)
            total += i;

        return total;
    }
}