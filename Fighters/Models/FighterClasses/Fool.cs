namespace Fighters.Models.FighterClasses;
internal class Fool : IFighterClasses
{
    public ClassesTypes ClassType => ClassesTypes.Universal;

    public int DamageBuff => 3;

    public int ArmorBuff => 1;

    public int HealthBuff => 0;

    public int NotSuitableWeaponDebuff => 0;
}
