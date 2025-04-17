using System.Xml.Linq;

namespace DictionaryTask.Storage
{
    internal class StorageFilePort : IStorage
    {
        private string _filePath = string.Empty;
        private char _state = '0';
        private StreamReader _reader;
        private StreamWriter _writer;
        public void Close()
        {
            _reader.Close();
            _writer.Close();
            _reader.Dispose();
            _writer.Dispose();
            _state = '0';
        }

        public StorageFilePort( string name )
        {
            if ( !TryOpen( name ) )
                TryCreate( name );
        }


        public bool TryCreate( string name )
        {
            if ( !File.Exists( name ) )
            {
                File.Create( name );
                _filePath = name;
                _reader = new StreamReader( _filePath );
                _state = 'r';
                return true;
            }
            return false;
        }

        public bool TryOpen( string path )
        {
            if ( File.Exists( path ) )
            {
                _filePath = path;
                _reader = new StreamReader( _filePath );
                _state = 'r';
                return true;
            }
            return false;
        }

        public bool TryReadline( out string line )
        {
            if ( _state == 'w' )
            {
                _writer.Dispose();
                _state = 'r';
            }
            string str = _reader.ReadLine();
            if ( str != null )
            {
                line = str;
                return true;
            }
            line = string.Empty;
            return false;
        }

        public void RewriteStorage( List<string> lines )
        {
            if ( _state == 'r' )
            {
                _reader.Dispose();
                _state = 'w';
            }
            using ( _writer = new StreamWriter( _filePath ) )
            {
                foreach ( string line in lines )
                {
                    _writer.WriteLine( line );
                }
            }
        }
    }
}
