using SupLanguages = DictionaryTask.DictionaryLogic.LanguagesInfo.SupportedLanguages;
namespace DictionaryTask.DictionaryService
{
    internal interface IDictionaryServiceManager
    {
        public void AddWordUseCase( SupLanguages mainLang, string mainWord, SupLanguages translatedLang, string translationWord );
        public bool GetTranslationUseCase( SupLanguages mainLang, string word, SupLanguages translatedLang, out string translation );
        public void LoadDictionariesFromStorageUseCase();
        public void SaveDictionariesToStorageUseCase();

        public List<string> GetDictionaryToString();

        public void CloseStorage();
    }
}
