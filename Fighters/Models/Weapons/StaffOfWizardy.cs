namespace Fighters.Models.Weapons;
internal class StaffOfWizardy : IWeapon
{
    public int Damage
    {
        get => 5;
    }
    public int CriticalDamagePercentage
    {
        get => 100;
    }
    public double CriticalDamageChance
    {
        get => 0.2;
    }

    public WeaponsTypes Type => WeaponsTypes.Magic;
}