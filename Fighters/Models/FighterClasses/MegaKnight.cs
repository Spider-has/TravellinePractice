namespace Fighters.Models.FighterClasses;

public class MegaKnight : IFighterClasses
{
    public ClassesTypes ClassType => ClassesTypes.Melee;

    public int DamageBuff => 5;

    public int ArmorBuff => 3;

    public int HealthBuff => 10;

    public int NotSuitableWeaponDebuff => 3;
}