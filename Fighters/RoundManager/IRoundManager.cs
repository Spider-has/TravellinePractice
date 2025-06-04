using Fighters.Models.Fighters;

namespace Fighters.RoundManager;
public interface IRoundManager
{
    public IFighter PlayRoundUseCase( IFighter fighterA, IFighter fighterB );
}
