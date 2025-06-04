namespace Fighters.Models.Races;

public class Human : IRace
{
    public int Damage => 1;

    public int Health => 100;

    public int Armor => 2;

    public double CriticalDamageChance => 0.05;

    public int CriticalDamagePercentage => 10;

}
