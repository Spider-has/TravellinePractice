
namespace CarFactory.Models.Transmission
{
    internal class SingleStage : ITransmission
    {
        public double Efficiency => 0.97;

        public double FuelConsumptionBuff => 0;
        public int GearsCount => 1;
    }
}
