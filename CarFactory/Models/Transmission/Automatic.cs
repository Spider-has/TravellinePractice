
namespace CarFactory.Models.Transmission
{
    internal class Automatic : ITransmission
    {
        public double Efficiency => 0.9;

        public double FuelConsumptionBuff => 1.5;

        public int GearsCount => 8;

    }
}
