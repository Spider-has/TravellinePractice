namespace Dictionary
{
    internal class EngRusDictionary
    {
        private Dictionary<string, string> _dictionary;

        public EngRusDictionary()
        {
            _dictionary = new Dictionary<string, string>();
        }
        public void AddWordsCouple( string rus, string eng ) => _dictionary[ rus.ToLower() ] = eng.ToLower();
        public bool GetEngByRusWord( string rus, out string translation )
        {
            if ( _dictionary.ContainsKey( rus ) )
            {
                translation = _dictionary[ rus ];
                return true;
            }
            translation = string.Empty;
            return false;
        }
        public bool GetRusByEngWord( string translation, out string rus )
        {
            if ( _dictionary.ContainsValue( translation ) )
            {
                rus = _dictionary.FirstOrDefault( x => x.Value == translation ).Key;
                return true;
            }
            rus = string.Empty;
            return false;
        }

        public void PrintAllWords()
        {
            foreach ( var pair in _dictionary )
            {
                Console.WriteLine( $"key: {pair.Key}  value: {pair.Value}" );
            }
        }

        public List<string> DictionaryToStringsFileFormat()
        {
            List<string> strings = new();
            foreach ( var pair in _dictionary )
            {
                strings.Add( $"{pair.Key}:{pair.Value}" );
            }
            return strings;
        }
    }
}
