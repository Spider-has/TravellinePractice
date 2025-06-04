namespace Fighters.Models.Weapons;

public interface IWeapon
{
    public int Damage { get; }
    public int CriticalDamagePercentage { get; }

    public double CriticalDamageChance { get; }

    public WeaponsTypes Type { get; }
}