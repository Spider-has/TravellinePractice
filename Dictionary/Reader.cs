namespace Dictionary
{
    internal class Reader
    {
        public static string ReadNonEmptyString()
        {
            string input = Console.ReadLine();
            while ( string.IsNullOrWhiteSpace( input ) )
            {
                Console.WriteLine( "Пожалуйста, введите не пустую строку и не пробелы" );
                input = Console.ReadLine();
            }
            return input;
        }

        public static string ReadNonEmptyRussianString()
        {
            string input = Console.ReadLine();
            while ( string.IsNullOrWhiteSpace( input ) || !LanguageChecker.IsWordRus( input ) )
            {
                Console.WriteLine( "Пожалуйста, введите руссое слово" );
                input = Console.ReadLine();
            }
            return input;
        }

        public static string ReadNonEmptyEnglishString()
        {
            string input = Console.ReadLine();
            while ( string.IsNullOrWhiteSpace( input ) || !LanguageChecker.IsWordEng( input ) )
            {
                Console.WriteLine( "Пожалуйста, введите английское слово" );
                input = Console.ReadLine();
            }
            return input;
        }

        public static bool ReadQuestionAnswer()
        {
            string answer = Console.ReadLine();
            while ( answer.ToLower() != "y" && answer.ToLower() != "n" )
            {
                Console.WriteLine( "Пожалуйста, введите либо 'y', либо 'n'" );
                answer = ReadNonEmptyString();
            }
            return answer.ToLower() == "y";
        }
    }
}
