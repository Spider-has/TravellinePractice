namespace DictionaryTask.DictionaryLogic
{
    internal interface IDictionaries
    {
        public bool TryAddWordsCouple( LanguagesInfo.SupportedLanguages lang1, string word, LanguagesInfo.SupportedLanguages lang2, string translation );

        public bool TryGetTranslation( LanguagesInfo.SupportedLanguages mainLang, string word, LanguagesInfo.SupportedLanguages translatedLang, out string translated );

        public List<string> GetAllWordsPairs();
    }
}
