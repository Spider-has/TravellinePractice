
namespace CarFactory.Models.BodyShape;

public class Couple : IBodyShape
{
    public double AerodynamicCoef => 0.3;

    public double EsteticCoef => 0.6;

    public int Weight => 1400;
}
