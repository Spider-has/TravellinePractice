
namespace CarFactory.Models.Transmission
{
    public class Manual : ITransmission
    {
        public double Efficiency => 0.95;

        public double FuelConsumptionBuff => 0.5;

        public int GearsCount => 6;
    }
}
