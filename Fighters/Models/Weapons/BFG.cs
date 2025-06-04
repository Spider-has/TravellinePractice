namespace Fighters.Models.Weapons;
public class BFG: IWeapon
{
    public int Damage
    {
        get => 100;
    }
    public int CriticalDamagePercentage
    {
        get => 0;
    }
    public double CriticalDamageChance
    {
        get => 0;
    }

    public WeaponsTypes Type => WeaponsTypes.Range;
}