namespace Fighters.Models.Fighters;
public struct FigherAttakInfo(AttackTypes type, int damage)
{
    public AttackTypes AttackType = type;
    public int TotalDamage = damage;
}

public enum AttackTypes  {
    Miss,
    Knock,
    CriticalKnock,
    UnPenetrate,
}
