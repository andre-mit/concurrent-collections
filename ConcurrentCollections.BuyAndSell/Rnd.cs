namespace ConcurrentCollections.BuyAndSell;

public class Rnd
{
    private static Random _generator = new Random();
    public static int NextInt(int max) => _generator.Next(max);
    public static bool TrueWithProbability(double probability) => _generator.NextDouble() < probability;
}