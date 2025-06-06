
using CarFactory.Models.BodyShape;
using CarFactory.Models.Color;
using CarFactory.Models.Engine;
using CarFactory.Models.Transmission;
using static CarFactory.Configs.ConsoleCommandsConfig;

namespace CarFactory.Configs;
public class CarsDataConfig
{
    public enum AvailiableBodyShapes
    {
        Couple,
        Jeep,
        Limousine,
        Pickup,
        Sedan,
    }
    public struct BodyShapesData( AvailiableBodyShapes type, string name, IBodyShape bodyShape )
    {
        public AvailiableBodyShapes type = type;
        public string bodyShapeName = name;
        public IBodyShape bodyShape = bodyShape;
    }

    public static readonly List<BodyShapesData> BodyShapesList =
    [
        new(AvailiableBodyShapes.Couple, "Купе", new Couple()),
        new(AvailiableBodyShapes.Jeep, "Джип", new Jeep()),
        new(AvailiableBodyShapes.Limousine, "Лимузин", new Limousine()),
        new(AvailiableBodyShapes.Pickup, "Пикап", new Pickup()),
        new(AvailiableBodyShapes.Sedan, "Седан", new Sedan()),
    ];


    public enum AvailiableEngines
    {
        Diesel,
        Electric,
        Hydrogen,
        Petrol,
    }
    public struct EnginesData( AvailiableEngines type, string name, IEngine engine )
    {
        public AvailiableEngines type = type;
        public string engineName = name;
        public IEngine engine = engine;
    }

    public static readonly List<EnginesData> EnginesList =
    [
        new(AvailiableEngines.Electric, "Электро-двигатель", new Electric()),
        new(AvailiableEngines.Diesel, "Дизельный двигатель", new Diesel()),
        new(AvailiableEngines.Hydrogen, "Водородный двигатель", new Hydrogen()),
        new(AvailiableEngines.Petrol, "Бензиновый двигатель", new Petrol()),
    ];


    public enum AvailiableTransmissions
    {
        Automatic,
        CVT,
        Manual,
        SingleStage,
    }
    public struct TransmissionData( AvailiableTransmissions type, string name, ITransmission transmission )
    {
        public AvailiableTransmissions type = type;
        public string transmissionName = name;
        public ITransmission transmission = transmission;
    }

    public static readonly List<TransmissionData> TransmissionsList =
    [
        new(AvailiableTransmissions.Automatic, "Автоматическая коробка передач", new Automatic()),
        new(AvailiableTransmissions.CVT, "Вариатор", new CVT()),
        new(AvailiableTransmissions.Manual, "Ручная коробка передач", new Manual()),
        new(AvailiableTransmissions.SingleStage, "Одноступенчатая коробка передач", new SingleStage()),
    ];


    public static readonly Dictionary<PaintTypes, string> PaintTypesList = new()
    {
        { PaintTypes.Acrylic, "Акрил" },
        { PaintTypes.Metallic, "Металлика" },
        { PaintTypes.Matte, "Матовый" },
        { PaintTypes.Chrome, "Хром" },
        { PaintTypes.Chameleon, "Хамелеон" },
    };

    public static readonly Dictionary<ColorTypes, string> ColorTypesList = new()
    {
        { ColorTypes.Blue, "Синий" },
        { ColorTypes.Orange, "Оранжевый" },
        { ColorTypes.Red, "Красный" },
        { ColorTypes.White, "Белый" },
        { ColorTypes.Black, "Черный" },
        { ColorTypes.Gray, "Серый" },
        { ColorTypes.Green, "Зеленый" },
        { ColorTypes.Cyan, "Цвет морской" },
        { ColorTypes.Yellow, "Желтый" },
    };

}
