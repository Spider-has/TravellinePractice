namespace Fighters.Models.Races;

public interface IRace
{
    public int Damage { get; }
    public int Health { get; }
    public int Armor { get; }

    public double CriticalDamageChance { get; }
    public int CriticalDamagePercentage { get; }
}