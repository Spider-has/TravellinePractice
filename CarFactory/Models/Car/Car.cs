
using CarFactory.Models.BodyShape;
using CarFactory.Models.Color;
using CarFactory.Models.Engine;
using CarFactory.Models.Transmission;

namespace CarFactory.Models.Car;

public class CarConstructor : ICar
{
    public string Name { get; }

    private readonly IEngine _engine;
    private readonly IColor _color;
    private readonly IBodyShape _bodyShape;
    private readonly ITransmission _transmission;

    private int _maxSpeed;
    private double _fuelConsumption;

    public CarConstructor( string name, PaintTypes paintType, ColorTypes colorType, IEngine engine, IBodyShape bodyShape, ITransmission transmission )
    {
        _engine = engine;
        _color = new CarColor( paintType, colorType );
        _bodyShape = bodyShape;
        _transmission = transmission;

        Name = name;


        // Такой показатель и расхода топлива скорости высчитывается абсолютно случайно по субъективному восприятию этого мира автором кода
        // Проще говоря, я вообще не разбираюсь в автомобилях.
        _maxSpeed = Math.Max( ( int )Math.Round( ( _engine.AvaragePower * 8 - _bodyShape.Weight ) * _engine.AvarageEfficiency * _transmission.Efficiency * ( 1 - _bodyShape.AerodynamicCoef ) ), 0 );

        _fuelConsumption = ( _engine.AvaragePower * _engine.AvarageEfficiency / 100 ) + _transmission.FuelConsumptionBuff;
    }

    public double GetFuelConsumption() => _fuelConsumption;
    public IBodyShape GetBodyShape() => _bodyShape;

    public IColor GetColor() => _color;

    public IEngine GetEngine() => _engine;
    public ITransmission GetTransmission() => _transmission;

    public int GetMaxSpeed() => _maxSpeed;

    public int GetGearsCount() => _transmission.GearsCount;

    public double GetEsteticCoef() => _bodyShape.EsteticCoef;

}
