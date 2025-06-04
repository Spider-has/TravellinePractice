namespace Fighters.Models.Races;
internal class Undead : IRace
{
    public int Damage => 3;

    public int Health => 140;

    public int Armor => 0;

    public double CriticalDamageChance => 0.15;

    public int CriticalDamagePercentage => 15;

}