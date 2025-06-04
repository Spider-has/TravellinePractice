
namespace Fighters.Models.FighterClasses;
internal class AntiMage : IFighterClasses
{
    // Просто крип
    public ClassesTypes ClassType => ClassesTypes.Melee;

    public int DamageBuff => 0;

    public int ArmorBuff => 0;

    public int HealthBuff => 0;

    public int NotSuitableWeaponDebuff => 0;
}