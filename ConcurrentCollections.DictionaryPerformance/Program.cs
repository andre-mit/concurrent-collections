using ConcurrentCollections.DictionaryPerformance;
using System.Collections.Concurrent;

int dictSize = 200000;

WriteLine("Dictionary, single thread:");
var dict = new Dictionary<int, int>();
SingleThreadBenchmark.Benchmark(dict, dictSize);


WriteLine("\r\nConcurrentDictionary, single thread:");
var cdict = new ConcurrentDictionary<int, int>();
SingleThreadBenchmark.Benchmark(cdict, dictSize);

WriteLine("\r\nConcurrentDictionary, multiple threads:");
var cdict2 = new ConcurrentDictionary<int, int>();
ParallelBenchmark.Benchmark(cdict2, dictSize);