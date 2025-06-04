namespace Fighters.CommunicationUI;

public interface ICommunicationUI
{
    public string ReadLine();
    public void WriteLine( string text );
    public void Clear();
}
