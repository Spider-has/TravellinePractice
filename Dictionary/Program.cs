
// Интерпретиловал "слово", как строгую последовательность букв (возможен дефис)
// Соответсвенно "русское слово" -- буквы а..я, А..Я и дефис
// "английское слово" -- буквы a..z, A..Z, дефис
using CommunicationUI;
using CommunicationUI.CommunicationUIConsolePort;

namespace DictionaryTask
{
    internal class Program
    {
        public static void Main()
        {
            ICommunicationUI consoleCommunication = new CommunicationUIConsolePort();
            App app = new App( consoleCommunication );

            consoleCommunication.WriteLine( $"Добро пожаловать! Здесь вы можете переводить слова с разных языков!{Environment.NewLine}Для этого просто введите слово на любом из языков, в ответе выведется перевод{Environment.NewLine}Если такого слова ещё нет в нашем словаре, вы можете вручною задать ему перевод!" );

            app.Run();

            consoleCommunication.WriteLine( "До новых встреч!" );
        }
    }
}
