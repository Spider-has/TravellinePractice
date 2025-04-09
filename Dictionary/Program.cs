
// Интерпретиловал "слово", как строгую последовательность букв (возможен дефис)
// Соответсвенно "русское слово" -- буквы а..я, А..Я и дефис
// "английское слово" -- буквы a..z, A..Z, дефис

namespace Dictionary
{
    enum Language
    {
        Non,
        ENG,
        RUS
    }
    internal class Program
    {
        static EngRusDictionary Dictionary = new();

        static public void AddNewWordsCouple( Language lang, string word )
        {
            Console.Write( $"Введите перевод слова {word}: " );
            string translatedWord = "";
            switch ( lang )
            {
                case Language.ENG:
                    translatedWord = Reader.ReadNonEmptyRussianString();
                    Dictionary.AddWordsCouple( translatedWord, word );
                    break;
                case Language.RUS:
                    translatedWord = Reader.ReadNonEmptyEnglishString();
                    Dictionary.AddWordsCouple( word, translatedWord );
                    break;
            }
        }
        static public void CheckWordProcess( string word )
        {
            string translation = "";
            Language lang = Language.Non;
            bool exists = false;
            if ( LanguageChecker.IsWordEng( word ) )
            {
                exists = Dictionary.GetRusByEngWord( word, out translation );
                lang = Language.ENG;
            }
            else if ( LanguageChecker.IsWordRus( word ) )
            {
                exists = Dictionary.GetEngByRusWord( word, out translation );
                lang = Language.RUS;
            }
            if ( exists )
            {
                Console.WriteLine( $"Перевод: {translation}" );
            }
            else if ( lang != Language.Non )
            {
                Console.WriteLine( "\nУ нас ещё нет перевода для такого слова, желаете добавить? (Y / N) " );
                if ( Reader.ReadQuestionAnswer() )
                {
                    AddNewWordsCouple( lang, word );
                }
            }
            else
                Console.WriteLine( "Похоже, то, что вы ввели не является словом " );
        }

        public static void Main()
        {
            DictionaryFileManager.InitDictionary( ref Dictionary );

            Console.WriteLine( "Добро пожаловать! Здесь вы можете переводить слова с русского на английский и наоборот!\n
            Для этого просто введите слово на любом из языков, в ответе выведется перевод\n
            Если такого слова ещё нет в нашем словаре, вы можете вручною задать ему перевод!\n
            \nДля выхода из режима ввода слов введите команду -exit в поле ввода слова\n" );
            bool isContinued = true;
            while ( isContinued )
            {
                Console.Write( "\nВведите слово: " );
                string word = Reader.ReadNonEmptyString();
                if ( word != "-exit" )
                    CheckWordProcess( word );
                else
                    isContinued = false;
            }
            DictionaryFileManager.FillFileFromDictionary( ref Dictionary );
            Console.WriteLine( "До новых встреч!" );
        }
    }
}
