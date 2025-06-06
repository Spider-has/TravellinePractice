namespace CarFactory.Models.Transmission
{
    internal class CVT : ITransmission
    {
        public double Efficiency => 0.85;

        public double FuelConsumptionBuff => 0.6;

        public int GearsCount => 0;
    }
}
