using SupLanguages = DictionaryTask.DictionaryLogic.LanguagesInfo.SupportedLanguages;
namespace DictionaryTask.DictionaryLogic
{
    internal interface ILanguageChecker
    {
        public bool IsWordBelondsToLanguage( SupLanguages lang, string word );
        public bool TryIdentifyLanguageByName( string languageName, out SupLanguages lang );
    }
}
