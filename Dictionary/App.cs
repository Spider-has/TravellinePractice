using System.Text;
using CommunicationUI;
using DictionaryTask.DictionaryLogic;
using DictionaryTask.DictionaryService;
using InputValidationUI;
using InputValidationUI.Port;
using SupLanguages = DictionaryTask.DictionaryLogic.LanguagesInfo.SupportedLanguages;

namespace DictionaryTask
{
    internal class App
    {
        private readonly ICommunicationUI _console;
        private readonly IInputValidationUI _input;
        private readonly IDictionaryServiceManager _dictionaryServiceManager;
        private readonly ILanguageChecker _languageChecker;

        private List<SupLanguages> _allLanguageOptionsList;
        private List<string> _languagesNamesList = [];

        static readonly string _dictionaryPath = "dictionary.txt";

        static readonly string _mainLanguageSelectSuggesionMessage = "Выберите язык, на котором будете писать слова: ";
        static readonly string _translatedLanguageSelectSuggesionMessage = "Выберите язык, на который нужно перевести слова: ";
        static readonly string _addNewWordMessage = "Похоже такого слова ещё не существует, желаете добавить?(Y / N) ";
        static readonly string _enterWordTranslationMessage = "Введите перевод слова:";
        static readonly string _enterWordMessage = "Введите перевод слова:";

        static readonly string _nextIterationOptionsSelected = "Выберите, что вы будете делать дальше: ";
        static readonly string _continueOption = "Продолжить вводить слова на том же языке";
        static readonly string _changeLanguageOption = "Сменить язык ввода и перевода";
        static readonly string _exitProgramOption = "Завершить работу программы";
        static List<string> _nextIterationsOptions = [ _continueOption, _changeLanguageOption, _exitProgramOption ];

        public App( ICommunicationUI console )
        {
            _languageChecker = new LanguageChecker();
            _console = console;
            _input = new InputValidationUIPort( _console, _languageChecker );
            _dictionaryServiceManager = new DictionaryServiceManager( _dictionaryPath, _console, _languageChecker );
            InitLanguageParams();
        }

        private void InitLanguageParams()
        {
            _allLanguageOptionsList = LanguagesInfo.GetLanguagesList();
            _languagesNamesList = [];

            foreach ( SupLanguages language in _allLanguageOptionsList )
            {
                _languagesNamesList.Add( LanguagesInfo.languagesNames[ language ] );
            }
        }

        private void LanguageSelection( out SupLanguages mainLang, out SupLanguages translationLang )
        {
            int selectedLanguage = _input.GetOptionsInput( _mainLanguageSelectSuggesionMessage, _languagesNamesList );
            int selectedTranslationLanguage = _input.GetOptionsInput( _translatedLanguageSelectSuggesionMessage, _languagesNamesList );
            mainLang = _allLanguageOptionsList[ selectedLanguage ];
            translationLang = _allLanguageOptionsList[ selectedTranslationLanguage ];
        }

        private void AddNewWordWithInput( SupLanguages mainLang, string word, SupLanguages translationLang )
        {
            if ( _input.GetAnswerInput( _addNewWordMessage ) )
            {
                string translated = _input.GetLanguagedWordInput( _enterWordTranslationMessage, translationLang );
                _dictionaryServiceManager.AddWordUseCase( mainLang, word, translationLang, translated );
            }
        }

        private void NextIterationSelection( ref SupLanguages mainLang, ref SupLanguages translationLang, ref bool Running )
        {
            int selectedOption = _input.GetOptionsInput( _nextIterationOptionsSelected, _nextIterationsOptions );
            switch ( selectedOption )
            {
                case 1:
                    LanguageSelection( out mainLang, out translationLang );
                    break;
                case 2: Running = false; break;
            }
        }

        public void Run()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            _dictionaryServiceManager.LoadDictionariesFromStorageUseCase();
            bool isRunning = true;
            SupLanguages mainLang;
            SupLanguages translationLang;
            LanguageSelection( out mainLang, out translationLang );
            while ( isRunning )
            {
                string word = _input.GetLanguagedWordInput( _enterWordMessage, mainLang );
                if ( _dictionaryServiceManager.GetTranslationUseCase( mainLang, word, translationLang, out string translation ) )
                {
                    _console.WriteLine( translation );
                }
                else
                {
                    AddNewWordWithInput( mainLang, word, translationLang );
                }
                NextIterationSelection( ref mainLang, ref translationLang, ref isRunning );
            }
            _dictionaryServiceManager.SaveDictionariesToStorageUseCase();
            _dictionaryServiceManager.CloseStorage();
        }
    }
}
