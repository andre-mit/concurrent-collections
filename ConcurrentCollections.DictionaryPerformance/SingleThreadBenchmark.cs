using System.Diagnostics;

namespace ConcurrentCollections.DictionaryPerformance;

public class SingleThreadBenchmark
{
    static void Populate(IDictionary<int, int> dict, int dictSize)
    {
        for (int i = 0; i < dictSize; i++)
        {
            dict.Add(i, 1);
            Worker.DoSomething();
        }
    }

    static int Enumerate(IDictionary<int, int> dict)
    {
        int total = 0;
        foreach (var item in dict)
        {
            total += item.Value;
            Worker.DoSomething();
        }
        return total;
    }

    static int Lookup(IDictionary<int, int> dict)
    {
        int total = 0;
        int count = dict.Count;
        for (int i = 0; i < count; i++)
        {
            total += dict[i];
            Worker.DoSomething();
        }
        return total;
    }

    public static void Benchmark(IDictionary<int, int> dict, int dictSize)
    {
        var sw = Stopwatch.StartNew();
        Populate(dict, dictSize);
        WriteLine("Build: {0} ms", sw.ElapsedMilliseconds);

        sw.Restart();

        int total = Enumerate(dict);

        sw.Stop();

        WriteLine("Enumerate: {0} ms", sw.ElapsedMilliseconds);
        if (total != dictSize)
            WriteLine("ERROR: Total was {0}, expected {1}", total, dictSize);

        sw.Restart();
        total = Lookup(dict);
        sw.Stop();

        WriteLine("Lookup: {0} ms", sw.ElapsedMilliseconds);

        if (total != dictSize)
            WriteLine("Error: Total was {0}, expected {1}", total, dictSize);
    }
}