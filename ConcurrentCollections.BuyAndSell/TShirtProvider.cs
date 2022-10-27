using System.Collections.Immutable;

namespace ConcurrentCollections.BuyAndSell;

public static class TShirtProvider
{
    public static ImmutableArray<TShirt> AllShirts { get; } = ImmutableArray.Create(
        new TShirt("T001", "VS", 1050),
        new TShirt("T002", "VSCode", 1200),
        new TShirt("T003", "Docker", 1000),
        new TShirt("T004", "Kubernetes", 1000),
        new TShirt("T005", "Azure", 1000),
        new TShirt("T006", "C#", 1050),
        new TShirt("T007", "F#", 1200),
        new TShirt("T008", "Visual Studio", 1000),
        new TShirt("T009", "Visual Studio Code", 1000)
    );

    public static ImmutableDictionary<string, TShirt> AllShirtsByCode { get; } = AllShirts.ToImmutableDictionary(s => s.Code);

    public static TShirt SelectRandomShirt()
    {
        int selectedIndex = Rnd.NextInt(AllShirts.Length);
        return AllShirts[selectedIndex];
    }
}