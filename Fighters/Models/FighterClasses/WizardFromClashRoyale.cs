namespace Fighters.Models.FighterClasses;
internal class WizardFromClashRoyale : IFighterClasses
{
    public ClassesTypes ClassType => ClassesTypes.Magic;

    public int DamageBuff => 10;

    public int ArmorBuff => 1;

    public int HealthBuff => 5;

    public int NotSuitableWeaponDebuff => 7;
}