
using CarFactory.Models.BodyShape;
using CarFactory.Models.Car;
using CarFactory.Models.Color;
using CarFactory.Models.Engine;
using CarFactory.Models.Transmission;

namespace CarFactory;

public class CarManager
{
    private List<ICar> _cars = [];

    public List<ICar> GetCarsList() => _cars;

    public ICar AddNewCar( string name, PaintTypes paintType, ColorTypes colorType, IEngine engine, IBodyShape bodyShape, ITransmission transmission )
    {
        _cars.Add( new CarConstructor( name, paintType, colorType, engine, bodyShape, transmission ) );
        return _cars.Last();
    }
}
