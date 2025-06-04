

using Fighters.Models.Armors;
using Fighters.Models.FighterClasses;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.FightersManager;
public interface IFighterManager
{
    public void AddFighterUseCase( string name, IRace race, IFighterClasses fighterClass, IArmor? armor, IWeapon? weapon );
    public List<IFighter> GetAliveFightersUseCase();
    public void RemoveFighterFromListUseCase( IFighter fighter );
}
