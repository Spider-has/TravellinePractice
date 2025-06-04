using Fighters.Models.Armors;
using Fighters.Models.FighterClasses;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.FightersManager;
public class FightersManagerService: IFighterManager
{
    private readonly List<IFighter> _fighters = [];

    public void AddFighterUseCase(string name, IRace race, IFighterClasses fighterClass, IArmor? armor , IWeapon? weapon)
    {
        Fighter fighter = new Fighter( name, race, fighterClass );
        if(armor != null)
            fighter.SetArmor( armor );
        if( weapon != null )
            fighter.SetWeapon( weapon );
        _fighters.Add( fighter );
    }

    public List<IFighter> GetAliveFightersUseCase()
    {
        return _fighters;
    }

    public void RemoveFighterFromListUseCase(IFighter fighter)
    {
        _fighters.Remove( fighter );
    }
}
