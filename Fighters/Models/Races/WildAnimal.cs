namespace Fighters.Models.Races;
internal class WildAnimal : IRace
{
    public int Damage => 5;

    public int Health => 160;

    public int Armor => 1;

    public double CriticalDamageChance => 0.01;

    public int CriticalDamagePercentage => 20;

}
