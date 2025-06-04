using Fighters.Models.Fighters;

namespace Fighters.Extensions;

public static class IFighterExtensions
{
    private static Random random = new Random();
    private static int maxInitiative = 20;
    private static int minInitiative = 0;
    public static bool IsAlive( this IFighter fighter ) => fighter.GetCurrentHealth() > 0;
    public static int CalculateInitiative(this IFighter fighter)
    {
        return random.Next( minInitiative, maxInitiative + 1 );
    }
}