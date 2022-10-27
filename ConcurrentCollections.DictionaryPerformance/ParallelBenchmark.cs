using System.Collections.Concurrent;
using System.Diagnostics;

namespace ConcurrentCollections.DictionaryPerformance;

public class ParallelBenchmark
{
    static void Populate(ConcurrentDictionary<int, int> dict, int dictSize)
    {
        Parallel.For(0, dictSize, i =>
        {
            dict.TryAdd(i, 1);
            Worker.DoSomething();
        });
    }

    static int Enumerate(ConcurrentDictionary<int, int> dict)
    {
        int expectedTotal = dict.Count;

        int total = 0;

        Parallel.ForEach(dict,
            item =>
            {
                Interlocked.Add(ref total, item.Value);
                Worker.DoSomething();
            });


        return total;
    }

    static int Lookup(ConcurrentDictionary<int, int> dict)
    {
        int total = 0;
        int count = dict.Count;
        Parallel.For(0, count,
             (i) =>
             {
                 Interlocked.Add(ref total, dict[i]);
                 Worker.DoSomething();
             });

        return total;
    }

    public static void Benchmark(ConcurrentDictionary<int, int> dict, int dictSize)
    {
        Stopwatch sw = Stopwatch.StartNew();

        Populate(dict, dictSize);
        sw.Stop();
        WriteLine("Build: {0} ms", sw.ElapsedMilliseconds);

        sw.Restart();
        int total = Enumerate(dict);
        sw.Stop();

        WriteLine("Enumerate: {0} ms", sw.ElapsedMilliseconds);
        if (total != dictSize)
            WriteLine("Error: Total was {0}, expected {1}", total, dictSize);

        sw.Restart();
        int total2 = Lookup(dict);
        sw.Stop();
        WriteLine("Lookup: {0} ms", sw.ElapsedMilliseconds);
        if (total2 != dictSize)
            WriteLine("Error: Total was {0}, expected {1}", total, dictSize);
    }
}