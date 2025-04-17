using CommunicationUI;
using DictionaryTask.DictionaryLogic;
using DictionaryTask.Storage;
using SupLanguages = DictionaryTask.DictionaryLogic.LanguagesInfo.SupportedLanguages;
namespace DictionaryTask.DictionaryService
{
    internal class DictionaryServiceManager : IDictionaryServiceManager
    {
        static readonly string _wordSeparator = ":";
        static readonly string _languageSeparator = "+";

        static readonly string _errorAddNewWord = "Не удалось вставить слово в словарь, возможно указана неправильная пара языков";
        static readonly string _errorLanguageNameRead = "Не удалось определить язык, похоже такого языка не существует";

        private readonly IDictionaries _dictionary;
        private readonly IStorage _storage;
        private readonly ILanguageChecker _languageChecker;
        private readonly ICommunicationUI _logger;

        public DictionaryServiceManager( string storagePath, ICommunicationUI loggerPort, ILanguageChecker languageChecker )
        {
            _dictionary = new Dictionaries();
            _storage = new StorageFilePort( storagePath );
            _languageChecker = languageChecker;
            _logger = loggerPort;
        }
        public void AddWordUseCase( SupLanguages mainLang, string mainWord, SupLanguages translatedLang, string translationWord )
        {
            if ( !_dictionary.TryAddWordsCouple( mainLang, mainWord, translatedLang, translationWord ) )
            {
                _logger.WriteLine( _errorAddNewWord );
            }
        }

        public bool GetTranslationUseCase( SupLanguages mainLang, string word, SupLanguages translatedLang, out string translation )
        {
            return _dictionary.TryGetTranslation( mainLang, word, translatedLang, out translation );
        }

        private bool TryGetLanguageNamesFromLine( string line, out SupLanguages mainLang, out SupLanguages translatedLang )
        {
            string lang1 = line.Split( _languageSeparator )[ 0 ];
            string lang2 = line.Split( _languageSeparator )[ 1 ];
            if ( _languageChecker.TryIdentifyLanguageByName( lang1, out mainLang ) &&
                _languageChecker.TryIdentifyLanguageByName( lang2, out translatedLang ) )
            {
                return true;
            }
            translatedLang = SupLanguages.RUS;
            return false;
        }

        private bool TryAddWordsFromLine( string line, SupLanguages mainLang, SupLanguages translationLang )
        {
            string word1 = line.Split( _wordSeparator )[ 0 ];
            string word2 = line.Split( _wordSeparator )[ 1 ];
            if ( !_dictionary.TryAddWordsCouple( mainLang, word1, translationLang, word2 ) )
            {
                return false;
            }
            return true;
        }

        public void LoadDictionariesFromStorageUseCase()
        {
            SupLanguages mainLang = SupLanguages.RUS;
            SupLanguages translatedLang = SupLanguages.ENG;
            while ( _storage.TryReadline( out string line ) )
            {
                if ( line.Split( _languageSeparator ).Length == 2 )
                {
                    if ( !TryGetLanguageNamesFromLine( line, out mainLang, out translatedLang ) )
                        _logger.WriteLine( _errorLanguageNameRead );
                }
                else if ( line.Split( _wordSeparator ).Length == 2 )
                {
                    if ( !TryAddWordsFromLine( line, mainLang, translatedLang ) )
                        _logger.WriteLine( _errorAddNewWord );
                }
            }
        }

        public List<string> GetDictionaryToString()
        {
            return _dictionary.GetAllWordsPairs();
        }

        public void SaveDictionariesToStorageUseCase()
        {
            List<string> lines = _dictionary.GetAllWordsPairs();
            _storage.RewriteStorage( lines );
        }

        public void CloseStorage()
        {
            _storage.Close();
        }
    }
}
