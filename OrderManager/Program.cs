using CommunicationUI;
using CommunicationUI.CommunicationUIConsolePort;
using OrderManager;

public class Program
{
    static string _welcomeMessage = "Добро пожаловать в наш сервис заказов!";
    static string _goodbyeMessage = "Спасибо, что пользовались нашим приложением, до встречи!";
    public static void Main()
    {
        ICommunicationUI consoleUI = new CommunicationUIConsolePort();
        App app = new App( consoleUI );


        consoleUI.WriteLine( _welcomeMessage );

        app.Run();

        consoleUI.WriteLine( _goodbyeMessage );
    }

}