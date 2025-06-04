namespace Fighters.Models.Weapons;

public class Firsts : IWeapon
{
    public int Damage
    {
        get => 1;
    }
    public int CriticalDamagePercentage
    {
        get => 30;
    }
    public double CriticalDamageChance
    {
        get => 0.1;
    }

    public WeaponsTypes Type => WeaponsTypes.Melee;
}