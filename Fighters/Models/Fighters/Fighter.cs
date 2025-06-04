using Fighters.Models.Armors;
using Fighters.Models.FighterClasses;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;
public class Fighter : IFighter
{
    private readonly static int UpperDamageLimitPercentage = 120;
    private readonly static int LowerDamageLimitPercentage = 80;

    private readonly static double AttackMissChance = 0.1;

    private readonly static double DefaultCriticalDamageChance = 0.1;
    private readonly static int DefaultCriticalDamagePercentage = 120;

    private readonly static Random FighterRandomizer = new();

    private readonly IRace _race;
    private readonly IFighterClasses _fighterClass;

    private IArmor _armor = new NoArmor();
    private IWeapon _weapon = new Firsts();

    private int _currentHealth;

    private double _currentCriticalChance;
    private int _currentCriticalDamagePercentage;
    public string Name { get; private set; }

    public Fighter( string name, IRace race, IFighterClasses fighterClass )
    {

        Name = name;
        _race = race;
        _fighterClass = fighterClass;

        _currentHealth = GetMaxHealth();

        UpdateCurrentCriticalDamageParams();
    }

    private void UpdateCurrentCriticalDamageParams()
    {
        _currentCriticalChance = DefaultCriticalDamageChance + _race.CriticalDamageChance + _weapon.CriticalDamageChance;
        _currentCriticalDamagePercentage = DefaultCriticalDamagePercentage + _race.CriticalDamagePercentage + _weapon.CriticalDamagePercentage;
    }

    public int GetCurrentHealth() => _currentHealth;

    public int GetMaxHealth() => _race.Health + _fighterClass.HealthBuff;

    private bool IsAttackCritical() => FighterRandomizer.NextDouble() < _currentCriticalChance;

    private bool IsAttackMissed() => FighterRandomizer.NextDouble() < AttackMissChance;

    private int CalculateBasicDamage()
    {
        int damage = _race.Damage + _weapon.Damage;
        if ( ( int )_weapon.Type == ( int )_fighterClass.ClassType || _fighterClass.ClassType == ClassesTypes.Universal )
            damage += _fighterClass.DamageBuff;
        else
            damage -= _fighterClass.NotSuitableWeaponDebuff;
        return damage;
    }

    public FigherAttakInfo CalculateDamage()
    {
        int basicDamage = CalculateBasicDamage();
        int damageSpread = FighterRandomizer.Next( LowerDamageLimitPercentage, UpperDamageLimitPercentage + 1 );
        int totalDamage = ( int )Math.Round( ( basicDamage * damageSpread / 100.0 ) );

        if ( IsAttackCritical() )
        {
            totalDamage = ( int )Math.Round( ( totalDamage * _currentCriticalDamagePercentage / 100.0 ) );
            return new FigherAttakInfo( AttackTypes.CriticalKnock, totalDamage );
        }

        return new FigherAttakInfo( AttackTypes.Knock, totalDamage );
    }


    public void SetArmor( IArmor armor )
    {
        _armor = armor;
    }

    public void SetWeapon( IWeapon weapon )
    {
        _weapon = weapon;
        UpdateCurrentCriticalDamageParams();
    }

    public int GetCurrentArmor()
    {
        return _armor.Armor + _fighterClass.ArmorBuff + _race.Armor;
    }

    public FigherAttakInfo HandleEnemyAttack( int damage )
    {
        int fullArmor = GetCurrentArmor();
        if ( damage > fullArmor )
        {
            if(!IsAttackMissed())
            {
                int takenDamage = damage - fullArmor;
                int newHealth = _currentHealth - takenDamage;
                if ( newHealth < 0 )
                {
                    newHealth = 0;
                }
                _currentHealth = newHealth;
                return new FigherAttakInfo( AttackTypes.Knock, takenDamage );
            }
            else
                return new FigherAttakInfo( AttackTypes.Miss, 0 );
        }
        else
            return new FigherAttakInfo( AttackTypes.UnPenetrate, 0 );
    }
}
