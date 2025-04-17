namespace DictionaryTask.Storage
{
    internal interface IStorage
    {
        public bool TryCreate( string name );
        public bool TryOpen( string path );
        public bool TryReadline( out string line );
        public void RewriteStorage( List<string> lines );
        public void Close();
    }
}
