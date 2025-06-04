namespace Fighters.Models.Races;
public class Goblin : IRace
{
    public int Damage => 2;

    public int Health => 80;

    public int Armor => 1;

    public double CriticalDamageChance => 0.1;

    public int CriticalDamagePercentage => 25;

}
