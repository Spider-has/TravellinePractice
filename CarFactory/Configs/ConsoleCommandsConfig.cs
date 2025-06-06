
namespace CarFactory.Configs;
public static class ConsoleCommandsConfig
{
    public enum AvailaibleCommands
    {
        CreateCar,
        GetCarsList,
        CloseApp,
        Help,
        Clear,
    }
    public struct CommandData( AvailaibleCommands com, string name, string description )
    {
        public AvailaibleCommands command = com;
        public string commandName = name;
        public string commandDescription = description;
    }

    public static readonly List<CommandData> CommandsList =
    [
        new(AvailaibleCommands.Help, "help", "вызывает список доступных команд"),
        new(AvailaibleCommands.GetCarsList, "created-cars", "возвращает список всех созданных вами машин за эту сессию"),
        new(AvailaibleCommands.CloseApp, "exit", "выход из приложения"),
        new(AvailaibleCommands.CreateCar, "create-car", "создает машину с авторской конфигурацией"),
        new(AvailaibleCommands.Clear, "clear", "очищает экран"),
    ];
}





