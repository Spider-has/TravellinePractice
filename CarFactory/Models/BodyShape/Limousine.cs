namespace CarFactory.Models.BodyShape
{
    public class Limousine : IBodyShape
    {
        public double AerodynamicCoef => 0.39;

        public double EsteticCoef => 0.65;

        public int Weight => 3000;
    }
}
