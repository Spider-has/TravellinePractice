namespace DictionaryTask.DictionaryLogic
{
    class LanguagesInfo
    {
        public enum SupportedLanguages
        {
            RUS,
            ENG,
            MARI,
            GER
        }

        public static readonly Dictionary<SupportedLanguages, string> languagesNames = new()
        {
                { SupportedLanguages.RUS, "русский" },
                { SupportedLanguages.ENG, "английский" },
                { SupportedLanguages.MARI, "марийский" },
                { SupportedLanguages.GER, "немецкий" }
        };

        public static readonly Dictionary<SupportedLanguages, string> languagesAlphabets = new()
        {
                { SupportedLanguages.RUS, "абвгдеёжзийклмнопрстуфхцчшщъыьэюя"},
                { SupportedLanguages.ENG, "abcdefghijklmnopqrstuvwxyz" },
                { SupportedLanguages.MARI, "аӓбвгдеёжзийклмнҥоӧпрстуу̊ӱӱ̊фхцчшщъыӹьэюя" },
                { SupportedLanguages.GER, "aäbcdefghijklmnoöpqrstuüvwxyzäß" }
        };

        public static List<SupportedLanguages> GetLanguagesList()
        {
            return Enum.GetValues( typeof( SupportedLanguages ) ).Cast<SupportedLanguages>().ToList();
        }
    }
}