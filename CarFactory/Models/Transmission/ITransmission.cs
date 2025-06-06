
namespace CarFactory.Models.Transmission;

public interface ITransmission
{
    double Efficiency { get; }
    double FuelConsumptionBuff { get; }

    int GearsCount { get; }
}
