using SupLanguages = DictionaryTask.DictionaryLogic.LanguagesInfo.SupportedLanguages;

namespace DictionaryTask.DictionaryLogic
{
    internal class LanguageChecker : ILanguageChecker
    {
        private static readonly char[] _extraSymbols = "-".ToCharArray();

        public bool IsWordBelondsToLanguage( SupLanguages lang, string word )
        {
            string lowerWord = word.ToLower();
            if ( LanguagesInfo.languagesAlphabets.TryGetValue( lang, out string aphabet ) )
            {
                for ( int i = 0; i < lowerWord.Length; i++ )
                {
                    char letter = lowerWord[ i ];
                    if ( !( _extraSymbols.Contains( letter ) || aphabet.Contains( letter ) ) )
                        return false;
                }
                return true;
            }
            return false;
        }

        public bool TryIdentifyLanguageByName( string languageName, out SupLanguages lang )
        {
            List<SupLanguages> LangValues = LanguagesInfo.GetLanguagesList();
            for ( int i = 0; i < LangValues.Count; i++ )
            {
                if ( languageName == LanguagesInfo.languagesNames[ LangValues[ i ] ] )
                {
                    lang = LangValues[ i ];
                    return true;
                }
            }
            lang = SupLanguages.RUS;
            return false;
        }
    }
}
