using SupLanguages = DictionaryTask.DictionaryLogic.LanguagesInfo.SupportedLanguages;

namespace DictionaryTask.DictionaryLogic
{
    internal class Dictionaries : IDictionaries
    {
        private struct LangaugePair( SupLanguages main, SupLanguages translate )
        {
            public SupLanguages mainLang = main;
            public SupLanguages translationLang = translate;
        }

        private Dictionary<LangaugePair, Dictionary<string, string>> _dictionaries;

        public Dictionaries()
        {
            _dictionaries = [];
            InitDictionaries();
        }

        private void InitDictionaries()
        {
            List<SupLanguages> LangValues = LanguagesInfo.GetLanguagesList();
            for ( int i = 0; i < LangValues.Count - 1; i++ )
            {
                for ( int j = i + 1; j < LangValues.Count; j++ )
                {
                    _dictionaries.Add( new LangaugePair( LangValues[ i ], LangValues[ j ] ), [] );
                }
            }
        }

        private bool TryGetLangPair( LanguagesInfo.SupportedLanguages lang1, LanguagesInfo.SupportedLanguages lang2, out LangaugePair Pair )
        {
            LangaugePair langPair = new( lang1, lang2 );
            if ( _dictionaries.ContainsKey( langPair ) )
            {
                Pair = langPair;
                return true;
            }
            langPair.mainLang = lang2;
            langPair.translationLang = lang1;
            if ( _dictionaries.ContainsKey( langPair ) )
            {
                Pair = langPair;
                return true;
            }
            Pair = langPair;
            return false;
        }

        public bool TryAddWordsCouple( LanguagesInfo.SupportedLanguages lang1, string word, LanguagesInfo.SupportedLanguages lang2, string translation )
        {
            if ( TryGetLangPair( lang1, lang2, out LangaugePair pair ) )
            {
                if ( pair.mainLang == lang1 )
                    _dictionaries[ pair ].Add( word, translation );
                else
                    _dictionaries[ pair ].Add( translation, word );
                return true;
            }
            return false;
        }

        public bool TryGetTranslation( LanguagesInfo.SupportedLanguages mainLang, string word, LanguagesInfo.SupportedLanguages translatedLang, out string translated )
        {
            if ( TryGetLangPair( mainLang, translatedLang, out LangaugePair pair ) )
            {
                if ( pair.mainLang == mainLang && _dictionaries[ pair ].ContainsKey( word ) )
                {
                    translated = _dictionaries[ pair ][ word ];
                    return true;
                }
                if ( pair.mainLang == translatedLang && _dictionaries[ pair ].ContainsValue( word ) )
                {
                    translated = _dictionaries[ pair ].FirstOrDefault( x => x.Value == word ).Key;
                    return true;
                }
                translated = string.Empty;
                return false;
            }
            translated = string.Empty;
            return false;
        }

        public List<string> GetAllWordsPairs()
        {
            List<string> words = [];
            foreach ( KeyValuePair<LangaugePair, Dictionary<string, string>> langDictPair in _dictionaries )
            {
                LangaugePair pair = langDictPair.Key;
                words.Add( $"{LanguagesInfo.languagesNames[ pair.mainLang ]}+{LanguagesInfo.languagesNames[ pair.translationLang ]}" );
                foreach ( KeyValuePair<string, string> wordPair in langDictPair.Value )
                {
                    words.Add( $"{wordPair.Key}:{wordPair.Value}" );
                }
            }
            return words;
        }
    }
}
