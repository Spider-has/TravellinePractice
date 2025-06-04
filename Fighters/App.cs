using Fighters.CommunicationUI;
using Fighters.Configs;
using Fighters.FightersManager;
using Fighters.InputValidationUI;
using Fighters.Models.Armors;
using Fighters.Models.FighterClasses;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.RoundManager;


namespace Fighters;
internal class App
{
    private readonly static ICommunicationUI _console = new CommunicationUIConsolePort();
    private readonly static IInputValidationUI _inputs = new InputValidationUIPort( _console );

    private ConsoleCommandsConfig.AvailaibleCommands? _currentCommand;

    private readonly IRoundManager _roundManager = new RoundManagerService( _console );


    private readonly static IFighterManager _fighterManager = new FightersManagerService();

    private readonly static Random _random = new Random();

    private string AppState = "continued";

    List<ConsoleCommandsConfig.AvailaibleCommands> _commands = ConsoleCommandsConfig.GetAvailiableCommandsList();

    List<FightersConfig.AvailiableClasses> _availiableClasses = FightersConfig.GetAvailaibleClassesList();
    List<string> _availiableClassesNames = [];

    List<FightersConfig.AvailiableArmors> _availiableArmors = FightersConfig.GetAvailaibleArmorsList();
    List<string> _availiableArmorsNames = [];

    List<FightersConfig.AvailiableWeapons> _availiableWeapons = FightersConfig.GetAvailaibleWeaponsList();
    List<string> _availiableWeaponsNames = [];

    List<FightersConfig.AvailiableRaces> _availiableRaces = FightersConfig.GetAvailaibleRacesList();
    List<string> _availiableRacesNames = [];

    public App()
    {
        _availiableClasses.ForEach( ( el ) =>
        {
            _availiableClassesNames.Add( FightersConfig.AvailiableClassesNames[ el ] );
        } );

        _availiableArmors.ForEach( ( el ) =>
        {
            _availiableArmorsNames.Add( FightersConfig.AvailiableArmorsNames[ el ] );
        } );

        _availiableWeapons.ForEach( ( el ) =>
        {
            _availiableWeaponsNames.Add( FightersConfig.AvailiableWeaponsNames[ el ] );
        } );

        _availiableRaces.ForEach( ( el ) =>
        {
            _availiableRacesNames.Add( FightersConfig.AvailiableRacesNames[ el ] );
        } );
    }

    private void WriteCommandsList()
    {

        _console.WriteLine( MessagesConfig.CommandList );
        _commands.ForEach( command =>
        {
            _console.WriteLine( $" {ConsoleCommandsConfig.AvailCommandsNames[ command ]} - {ConsoleCommandsConfig.AvailCommandsDescriptions[ command ]}" );
        } );
    }

    private void AddNewFighter()
    {
        string name = _inputs.GetStringInput( MessagesConfig.AddingFighterName );

        int selectedClass = _inputs.GetOptionsInput( MessagesConfig.AddingFighterClass, _availiableClassesNames );
        _console.WriteLine( $"Вы выбрали класс: {FightersConfig.AvailiableClassesNames[ _availiableClasses[ selectedClass ] ]}" );

        IFighterClasses fighterClass = FightersConfig.AvailaibleClass[ _availiableClasses[ selectedClass ] ];

        int selectedRace = _inputs.GetOptionsInput( MessagesConfig.AddingFighterRace, _availiableRacesNames );
        _console.WriteLine( $"Вы выбрали расу: {FightersConfig.AvailiableRacesNames[ _availiableRaces[ selectedRace ] ]}" );

        IRace fighterRace = FightersConfig.AvailRaceByEnum[ _availiableRaces[ selectedRace ] ];

        IArmor? Armor = null;
        if ( _inputs.GetAnswerInput( MessagesConfig.IsArmorNeeded ) )
        {
            int selectedArmor = _inputs.GetOptionsInput( MessagesConfig.AddingFighterArmor, _availiableArmorsNames );
            _console.WriteLine( $"Вы выбрали броню: {FightersConfig.AvailiableArmorsNames[ _availiableArmors[ selectedArmor ] ]}" );

            Armor = FightersConfig.AvailArmorByEnum[ _availiableArmors[ selectedArmor ] ];
        }

        IWeapon? Weapon = null;
        if ( _inputs.GetAnswerInput( MessagesConfig.IsWeaponNeeded ) )
        {
            int selectedWeapon = _inputs.GetOptionsInput( MessagesConfig.AddingFighterWeapon, _availiableWeaponsNames );
            _console.WriteLine( $"Вы выбрали оружие: {FightersConfig.AvailiableWeaponsNames[ _availiableWeapons[ selectedWeapon ] ]}" );

            Weapon = FightersConfig.AvailWeaponByEnum[ _availiableWeapons[ selectedWeapon ] ];
        }

        _fighterManager.AddFighterUseCase( name, fighterRace, fighterClass, Armor, Weapon );
        _console.WriteLine( $"Боец {name} успешно создан!" );
    }

    public void CheckCommand( List<ConsoleCommandsConfig.AvailaibleCommands> commands, string inputMessage )
    {
        string commandStr = inputMessage.Trim();
        bool isCorrect = false;
        _currentCommand = null;
        commands.ForEach( command =>
        {
            if ( commandStr == ConsoleCommandsConfig.AvailCommandsNames[ command ] )
            {
                _currentCommand = command;
                isCorrect = true;
            }
            ;
        } );
        if ( !isCorrect )
            _console.WriteLine( MessagesConfig.IncorrectCommand );
    }

    private static void PrintAliveFighters()
    {
        _console.WriteLine( MessagesConfig.AliveFightersListCommand );

        List<IFighter> fighters = _fighterManager.GetAliveFightersUseCase();
        fighters.ForEach( ( fighter ) =>
        {
            _console.WriteLine( $"боец: {fighter.Name}, жизней осталось: {fighter.GetCurrentHealth()}" );
        } );
    }

    private void StartRound()
    {
        List<IFighter> fighters = _fighterManager.GetAliveFightersUseCase();
        if ( fighters.Count >= 2 )
        {
            int fighterAIndex = _random.Next( 0, fighters.Count );
            int fighterBIndex = fighterAIndex;

            while ( fighterBIndex == fighterAIndex )
            {
                fighterBIndex = _random.Next( 0, fighters.Count );
            }

            _console.WriteLine( $"\nСражается боец: {fighters[ fighterAIndex ].Name} и боец  {fighters[ fighterBIndex ].Name}. Да начнется битва!\n" );

            IFighter winner = _roundManager.PlayRoundUseCase(
                fighters[ fighterAIndex ],
                fighters[ fighterBIndex ] );
            IFighter loser;

            if ( winner == fighters[ fighterAIndex ] )
                loser = fighters[ fighterBIndex ];
            else
                loser = fighters[ fighterAIndex ];
            _console.WriteLine( $"Проигравший: {loser.Name}" );
            _console.WriteLine( $"Победитель: {winner.Name}, здоровья осталось: {winner.GetCurrentHealth()}\n" );
            _fighterManager.RemoveFighterFromListUseCase( loser );
        }
        else
            _console.WriteLine( MessagesConfig.NotEnoughFighters );
    }
    public void ExecuteCommand()
    {
        switch ( _currentCommand )
        {
            case ConsoleCommandsConfig.AvailaibleCommands.AddFighter:
                {
                    AddNewFighter();
                    break;
                }
            case ConsoleCommandsConfig.AvailaibleCommands.GetAliveFightersList:
                {
                    PrintAliveFighters();
                    break;
                }
            case ConsoleCommandsConfig.AvailaibleCommands.StartRound:
                {
                    StartRound();
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
        _console.WriteLine( MessagesConfig.WelcomeMessage );

        WriteCommandsList();

        while ( AppState == "continued" )
        {
            string inputStr = _inputs.GetStringInput( MessagesConfig.EnterCommand );
            CheckCommand( _commands, inputStr );
            ExecuteCommand();
        }

        _console.WriteLine( MessagesConfig.GoodbyeMessage );
    }
}
