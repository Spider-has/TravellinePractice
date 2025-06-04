using Fighters.Models.Weapons;

namespace Fighters.Models.FighterClasses;
public interface IFighterClasses
{
    public ClassesTypes ClassType { get; }
    public int DamageBuff { get; }

    public int NotSuitableWeaponDebuff { get; }

    public int ArmorBuff { get; }

    public int HealthBuff { get; }
}
