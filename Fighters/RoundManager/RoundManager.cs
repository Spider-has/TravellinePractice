using Fighters.CommunicationUI;
using Fighters.Extensions;
using Fighters.Models.Fighters;

namespace Fighters.RoundManager;

public class RoundManagerService( ICommunicationUI console ) : IRoundManager
{

    private readonly ICommunicationUI _communicationUI = console;

    private string HandleAttackType( AttackTypes type )
    {
        return type switch
        {
            AttackTypes.Knock => "обычную",
            AttackTypes.CriticalKnock => "КРИТИЧЕСКУЮ",
            _ => "",
        };
    }

    private void WriteAttackInfoMessage( IFighter attacker, FigherAttakInfo firstFighterDamage, IFighter defender, FigherAttakInfo enemyDamageTakenInfo )
    {
        switch ( enemyDamageTakenInfo.AttackType )
        {
            case AttackTypes.Knock:
                {
                    _communicationUI.WriteLine( $"{attacker.Name} нанес {HandleAttackType( firstFighterDamage.AttackType )} атаку по {defender.Name}, урона через броню нанесено: {enemyDamageTakenInfo.TotalDamage}" );
                    break;
                }
            case AttackTypes.Miss:
                {
                    _communicationUI.WriteLine( $"{attacker.Name} мог бы совершить {HandleAttackType( firstFighterDamage.AttackType )} атаку по {defender.Name} с уроном {firstFighterDamage.TotalDamage}, но ПРОМАЗАЛ!" );
                    break;
                }
            case AttackTypes.UnPenetrate:
                {
                    _communicationUI.WriteLine( $"{attacker.Name} нанес {HandleAttackType( firstFighterDamage.AttackType )} атаку по {defender.Name}, но этого оказалось недостаточно, чтобы ПРОБИТЬ БРОНЮ! урон: {firstFighterDamage.TotalDamage}, броня: {defender.GetCurrentArmor()}" );
                    break;
                }
        }
    }

    private void MakeAttack( IFighter attacker, IFighter defender )
    {
        FigherAttakInfo firstFighterDamage = attacker.CalculateDamage();
        FigherAttakInfo enemyDamageTakenInfo = defender.HandleEnemyAttack( firstFighterDamage.TotalDamage );
        WriteAttackInfoMessage(attacker, firstFighterDamage, defender, enemyDamageTakenInfo);
    }
    private IFighter? MakeAttacksIteration( IFighter fighterA, IFighter fighterB )
    {
        MakeAttack( fighterA, fighterB );
        if ( !fighterB.IsAlive() )
            return fighterA;

        MakeAttack( fighterB, fighterA );
        if ( !fighterA.IsAlive() )
            return fighterB;

        return null;
    }

    private IFighter PlayMatchAndGetWinner( IFighter firstAttackerFighter, IFighter secondAttackerFighter )
    {
        IFighter? winner = null;
        while ( winner == null )
            winner = MakeAttacksIteration( firstAttackerFighter, secondAttackerFighter );
        return winner;
    }

    public IFighter PlayRoundUseCase( IFighter fighterA, IFighter fighterB )
    {
        int firstInitive = fighterA.CalculateInitiative();
        _communicationUI.WriteLine( $"Инициатива бойца {fighterA.Name}: {firstInitive}" );
        int secondInitive = fighterA.CalculateInitiative();
        _communicationUI.WriteLine( $"Инициатива бойца {fighterB.Name}: {secondInitive}\n" );
        if ( firstInitive > secondInitive )
            return PlayMatchAndGetWinner( fighterA, fighterB );
        else
            return PlayMatchAndGetWinner( fighterB, fighterA );
    }
}