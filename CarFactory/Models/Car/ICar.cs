
using CarFactory.Models.BodyShape;
using CarFactory.Models.Color;
using CarFactory.Models.Engine;
using CarFactory.Models.Transmission;

namespace CarFactory.Models.Car
{
    public interface ICar
    {
        string Name { get; }

        public int GetMaxSpeed();

        public int GetGearsCount();
        public double GetEsteticCoef();

        public double GetFuelConsumption();

        public IEngine GetEngine();
        public IColor GetColor();
        public IBodyShape GetBodyShape();
        public ITransmission GetTransmission();

    }
}
