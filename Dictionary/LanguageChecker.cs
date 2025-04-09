namespace Dictionary
{
    internal class LanguageChecker
    {
        static char engStartLetter = 'a';
        static char engEndLetter = 'z';
        static char rusStartLetter = 'а';
        static char rusEndLetter = 'я';
        static char[] extraSymbols = "-".ToCharArray();
        public static bool IsWordEng( string word )
        {
            for ( int i = 0; i < word.Length; i++ )
            {
                char letter = word[ i ].ToString().ToLower()[ 0 ];
                if ( !( extraSymbols.Contains( letter ) || ( letter >= engStartLetter && letter <= engEndLetter ) ) )
                    return false;
            }
            return true;
        }

        public static bool IsWordRus( string word )
        {
            for ( int i = 0; i < word.Length; i++ )
            {
                char letter = word[ i ].ToString().ToLower()[ 0 ];
                if ( !( extraSymbols.Contains( letter ) || ( letter >= rusStartLetter && letter <= rusEndLetter ) ) )
                    return false;
            }
            return true;
        }
    }
}
