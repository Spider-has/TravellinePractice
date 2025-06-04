namespace Fighters.CommunicationUI;

internal class CommunicationUIConsolePort : ICommunicationUI
{
    public string ReadLine()
    {
        string? line = Console.ReadLine();
        if ( line == null )
            return "";
        return line;
    }
    public void WriteLine( string text )
    {
        Console.WriteLine( text );
    }

    public void Clear()
    {
        Console.Clear();
    }
}
