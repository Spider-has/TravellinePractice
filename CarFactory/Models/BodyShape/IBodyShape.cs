namespace CarFactory.Models.BodyShape
{
    public interface IBodyShape
    {
        public double AerodynamicCoef { get; }
        public double EsteticCoef { get; }
        public int Weight { get; }

    }
}
