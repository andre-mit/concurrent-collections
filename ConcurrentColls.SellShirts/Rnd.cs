namespace ConcurrentCollections.SellShirts;

public class Rnd
{
    private static Random _generator = new();
    public static int NextInt(int max) => _generator.Next(max);
    public static bool TrueWiithProbability(double probability) => _generator.NextDouble() < probability;
}