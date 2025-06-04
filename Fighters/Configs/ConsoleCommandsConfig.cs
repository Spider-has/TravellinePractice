using Fighters.Utils;

namespace Fighters.Configs;
public static class ConsoleCommandsConfig
{
    public enum AvailaibleCommands
    {
        AddFighter,
        GetAliveFightersList,
        StartRound,
        CloseApp,
        Help,
        Clear,
    }

    public static readonly Dictionary<AvailaibleCommands, string> AvailCommandsNames = new()
    {
        {AvailaibleCommands.Help, "help" },
        {AvailaibleCommands.GetAliveFightersList, "alive-fighters" },
        {AvailaibleCommands.StartRound, "start-round" },
        {AvailaibleCommands.CloseApp, "exit" },
        {AvailaibleCommands.AddFighter, "add-fighter" },
        {AvailaibleCommands.Clear, "clear" },
    };

    public static readonly Dictionary<AvailaibleCommands, string> AvailCommandsDescriptions = new()
    {
        {AvailaibleCommands.Help, "вызывает список доступных команд" },
        {AvailaibleCommands.StartRound, "начинает матч между двумя случайными бойцами из списка" },
        {AvailaibleCommands.CloseApp, "выход из приложения" },
        {AvailaibleCommands.AddFighter, "добавляет нового бойца в список" },
        {AvailaibleCommands.GetAliveFightersList, "возвращает список всех живых бойцов" },
        {AvailaibleCommands.Clear, "очищает экран" },
    };

    public static List<AvailaibleCommands> GetAvailiableCommandsList()
    {
        return EnumsToList.EnumToList<AvailaibleCommands>();
    }
}


