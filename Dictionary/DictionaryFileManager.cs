namespace Dictionary
{
    internal class DictionaryFileManager
    {
        static string _fileName = "dictionary.txt";
        static string separator = ":";
        public static void InitOneLineFromFile( ref EngRusDictionary dictionary, string line )
        {
            bool err = false;
            if ( line.Split( separator ).Length == 2 )
            {
                string rus = line.Split( separator )[ 0 ];
                string eng = line.Split( separator )[ 1 ];
                if ( LanguageChecker.IsWordRus( rus ) && LanguageChecker.IsWordEng( eng ) )
                {
                    dictionary.AddWordsCouple( rus, eng );
                }
                else err = true;
            }
            else err = true;
            if ( err )
            {
                Console.WriteLine( $"Возникли ошибки при чтении строки {line} из файла" );
            }
        }
        public static void FillDictionaryFromFile( ref EngRusDictionary dictionary, StreamReader readingFile )
        {
            string line;
            while ( ( line = readingFile.ReadLine() ) != null )
            {
                InitOneLineFromFile( ref dictionary, line );
            }
        }

        public static void FillFileFromDictionary( ref EngRusDictionary dictionary )
        {
            if ( CheckFile( _fileName ) )
                using ( StreamWriter writer = new StreamWriter( _fileName, false ) )
                {
                    List<string> strings = dictionary.DictionaryToStringsFileFormat();
                    foreach ( string s in strings )
                    {
                        writer.WriteLine( s );
                    }
                }
        }

        static public bool CheckFile( string path )
        {
            if ( File.Exists( path ) )
            {
                return true;
            }
            else
            {
                Console.WriteLine( $"не удалось открыть файл по пути {path}, файл не существует" );
                return false;
            }
        }

        public static void InitDictionary( ref EngRusDictionary dictionary )
        {

            if ( CheckFile( _fileName ) )
                using ( StreamReader reader = new StreamReader( _fileName ) )
                {
                    FillDictionaryFromFile( ref dictionary, reader );
                }
        }
    }
}
