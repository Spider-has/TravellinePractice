using Fighters.Models.Armors;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public interface IFighter
{
    string Name { get; }

    public int GetCurrentHealth();
    public int GetMaxHealth();
    public FigherAttakInfo CalculateDamage();

    public void SetArmor(IArmor armor);
    public void SetWeapon(IWeapon weapon);

    public FigherAttakInfo HandleEnemyAttack( int damage );
    public int GetCurrentArmor();
}