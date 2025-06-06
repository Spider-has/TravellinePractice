namespace CarFactory.Models.BodyShape
{
    public class Jeep : IBodyShape
    {
        public double AerodynamicCoef => 0.4;

        public double EsteticCoef => 0.51;

        public int Weight => 2100;
    }
}
