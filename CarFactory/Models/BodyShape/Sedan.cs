namespace CarFactory.Models.BodyShape
{
    public class Sedan : IBodyShape
    {
        public double AerodynamicCoef => 0.33;

        public double EsteticCoef => 0.4;

        public int Weight => 1500;
    }
}
