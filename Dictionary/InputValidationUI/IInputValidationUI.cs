using DictionaryTask.DictionaryLogic;

namespace InputValidationUI
{
    internal interface IInputValidationUI
    {
        public string GetStringInput( string label );

        public bool GetAnswerInput( string label );

        public int GetOptionsInput( string label, List<string> options );

        public string GetLanguagedWordInput( string label, LanguagesInfo.SupportedLanguages lang );
    }
}
