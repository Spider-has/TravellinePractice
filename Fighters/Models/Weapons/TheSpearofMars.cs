
namespace Fighters.Models.Weapons;
internal class TheSpearofMars : IWeapon
{
    public int Damage
    {
        get => 10;
    }
    public int CriticalDamagePercentage
    {
        get => 50;
    }
    public double CriticalDamageChance
    {
        get => 0.3;
    }

    public WeaponsTypes Type => WeaponsTypes.Melee;
}