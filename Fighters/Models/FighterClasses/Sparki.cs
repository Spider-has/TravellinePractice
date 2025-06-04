
namespace Fighters.Models.FighterClasses;
internal class Sparki : IFighterClasses
{
    public ClassesTypes ClassType => ClassesTypes.Range;

    public int DamageBuff => 20;

    public int ArmorBuff => 0;

    public int HealthBuff => 0;

    public int NotSuitableWeaponDebuff => 10;
}
