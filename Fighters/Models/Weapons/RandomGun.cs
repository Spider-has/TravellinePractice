namespace Fighters.Models.Weapons;
internal class RandomGun : IWeapon
{
    public int Damage
    {
        get => 2;
    }
    public int CriticalDamagePercentage
    {
        get => 1000;
    }
    public double CriticalDamageChance
    {
        get => 0.01;
    }

    public WeaponsTypes Type => WeaponsTypes.Range;
}
