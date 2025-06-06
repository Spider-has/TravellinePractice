
using CarFactory.CommunicationUI;
using CarFactory.Configs;
using CarFactory.InputValidationUI;
using CarFactory.Models.BodyShape;
using CarFactory.Models.Car;
using CarFactory.Models.Color;
using CarFactory.Models.Engine;
using CarFactory.Models.Transmission;
using CarFactory.Utils;

namespace CarFactory;

public class App
{
    private readonly static ICommunicationUI _console = new CommunicationUIConsolePort();
    private readonly static IInputValidationUI _inputs = new InputValidationUIPort( _console );

    private readonly static CarManager _carManager = new CarManager();

    private ConsoleCommandsConfig.AvailaibleCommands? _currentCommand;

    private string AppState = "continued";

    private readonly List<string> _bodyShapesNames = [];
    private readonly List<string> _enginesNames = [];
    private readonly List<string> _transmissionsNames = [];
    private readonly List<string> _paintTypeNames = [];
    private readonly List<string> _colorTypeNames = [];

    private readonly List<PaintTypes> _paintTypesList = EnumsToList.EnumToList<PaintTypes>();
    private readonly List<ColorTypes> _colorTypesList = EnumsToList.EnumToList<ColorTypes>();


    public App()
    {
        CarsDataConfig.BodyShapesList.ForEach( x => _bodyShapesNames.Add( x.bodyShapeName ) );
        CarsDataConfig.EnginesList.ForEach( x => _enginesNames.Add( x.engineName ) );
        CarsDataConfig.TransmissionsList.ForEach( x => _transmissionsNames.Add( x.transmissionName ) );

        _paintTypesList.ForEach( x => _paintTypeNames.Add( CarsDataConfig.PaintTypesList[ x ] ) );
        _colorTypesList.ForEach( x => _colorTypeNames.Add( CarsDataConfig.ColorTypesList[ x ] ) );
    }

    private void WriteCommandsList()
    {

        _console.WriteLine( MessagesConfig.CommandList );
        ConsoleCommandsConfig.CommandsList.ForEach( command => { _console.WriteLine( $"  {command.commandName} - {command.commandDescription}" ); } );
    }

    public void CheckCommand( string inputMessage )
    {
        string commandStr = inputMessage.Trim();
        bool isCorrect = false;
        _currentCommand = null;
        ConsoleCommandsConfig.CommandsList.ForEach( command =>
        {
            if ( commandStr == command.commandName )
            {
                _currentCommand = command.command;
                isCorrect = true;
            }
            ;
        } );
        if ( !isCorrect )
            _console.WriteLine( MessagesConfig.IncorrectCommand );
    }

    private static string GetNameByBodyShape( IBodyShape bodyShape )
    {
        string name = "";
        CarsDataConfig.BodyShapesList.ForEach( x => { if ( x.bodyShape.Equals( bodyShape ) ) name = x.bodyShapeName; } );
        return name;
    }

    private static string GetNameByEngine( IEngine engine )
    {
        string name = "";
        CarsDataConfig.EnginesList.ForEach( x => { if ( x.engine.Equals( engine ) ) name = x.engineName; } );
        return name;
    }

    private static string GetNameByTransmission( ITransmission transmission )
    {
        string name = "";
        CarsDataConfig.TransmissionsList.ForEach( x => { if ( x.transmission.Equals( transmission ) ) name = x.transmissionName; } );
        return name;
    }

    private static void PrintCarInfo( ICar car )
    {
        _console.WriteLine( $"Машина с названием {car.Name}, \n  форма кузова: {GetNameByBodyShape( car.GetBodyShape() )}, \n  вид двигателя: {GetNameByEngine( car.GetEngine() )}, \n  вид коробки передач: {GetNameByTransmission( car.GetTransmission() )}, \n  тип краски: {CarsDataConfig.PaintTypesList[ car.GetColor().PaintType ]}, \n  цвет: {CarsDataConfig.ColorTypesList[ car.GetColor().Color ]}, \n  максимальная скорость: {car.GetMaxSpeed()}, \n  расход топлива: {car.GetFuelConsumption()}, \n  количество передач: {car.GetGearsCount()}" );
    }

    private static void PrintAllCars()
    {
        _console.WriteLine( MessagesConfig.AllCarsListCommand );

        List<ICar> cars = _carManager.GetCarsList();
        cars.ForEach(  car  => { PrintCarInfo( car ); } );
    }

    private void CreateNewCar()
    {
        string name = _inputs.GetStringInput( MessagesConfig.CreateCarEnterName );

        int selectedBodyShape = _inputs.GetOptionsInput( MessagesConfig.CreateCarEnterBodyColor, _bodyShapesNames );
        _console.WriteLine( $"Вы выбрали форму кузова: {CarsDataConfig.BodyShapesList[ selectedBodyShape ].bodyShapeName}" );

        IBodyShape bodyShape = CarsDataConfig.BodyShapesList[ selectedBodyShape ].bodyShape;

        int selectedEngine = _inputs.GetOptionsInput( MessagesConfig.CreateCarSelectEngine, _enginesNames );
        _console.WriteLine( $"Вы выбрали двигатель: {CarsDataConfig.EnginesList[ selectedEngine ].engineName}" );

        IEngine engine = CarsDataConfig.EnginesList[ selectedEngine ].engine;

        int selectedTransmission = _inputs.GetOptionsInput( MessagesConfig.CreateCarEnterTransmission, _transmissionsNames );
        _console.WriteLine( $"Вы выбрали коробку передач: {CarsDataConfig.TransmissionsList[ selectedTransmission ].transmissionName}" );

        ITransmission transmission = CarsDataConfig.TransmissionsList[ selectedTransmission ].transmission;

        int selectedPaintType = _inputs.GetOptionsInput( MessagesConfig.CreateCarEnterBodyPaintType, _paintTypeNames );
        _console.WriteLine( $"Вы выбрали стиль покраски: {CarsDataConfig.PaintTypesList[ _paintTypesList[ selectedPaintType ] ]}" );

        PaintTypes paintType = _paintTypesList[ selectedPaintType ];

        int selectedColorType = _inputs.GetOptionsInput( MessagesConfig.CreateCarEnterBodyColor, _colorTypeNames );
        _console.WriteLine( $"Вы выбрали стиль покраски: {CarsDataConfig.ColorTypesList[ _colorTypesList[ selectedColorType ] ]}" );

        ColorTypes colorType = _colorTypesList[ selectedColorType ];

        ICar car = _carManager.AddNewCar( name, paintType, colorType, engine, bodyShape, transmission );

        _console.WriteLine( MessagesConfig.SuccessfulCreation );

        PrintCarInfo( car );

    }
    public void ExecuteCommand()
    {
        switch ( _currentCommand )
        {
            case ConsoleCommandsConfig.AvailaibleCommands.CreateCar:
                {
                    CreateNewCar();
                    break;
                }
            case ConsoleCommandsConfig.AvailaibleCommands.GetCarsList:
                {
                    PrintAllCars();
                    break;
                }
            case ConsoleCommandsConfig.AvailaibleCommands.CloseApp:
                {
                    AppState = "closed";
                    break;
                }
            case ConsoleCommandsConfig.AvailaibleCommands.Help:
                {
                    WriteCommandsList();
                    break;
                }
            case ConsoleCommandsConfig.AvailaibleCommands.Clear:
                {
                    _console.Clear();
                    WriteCommandsList();
                    break;
                }
            default:
                break;
        }
    }


    public void Run()
    {
        _console.WriteLine( MessagesConfig.Greetings );

        WriteCommandsList();

        while ( AppState == "continued" )
        {
            string inputStr = _inputs.GetStringInput( MessagesConfig.EnterCommand );
            CheckCommand( inputStr );
            ExecuteCommand();
        }

        _console.WriteLine( MessagesConfig.GoodbyeMessage );
    }
}
