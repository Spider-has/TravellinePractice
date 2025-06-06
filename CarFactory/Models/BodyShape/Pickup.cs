
namespace CarFactory.Models.BodyShape
{
    public class Pickup : IBodyShape
    {
        public double AerodynamicCoef => 0.45;

        public double EsteticCoef => 0.55;

        public int Weight => 2500;
    }
}
