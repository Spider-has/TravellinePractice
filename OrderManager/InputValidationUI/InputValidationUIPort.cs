using CommunicationUI;

namespace InputValidationUI.Port
{
    internal class InputValidationUIPort( ICommunicationUI communicationUI ) : IInputValidationUI
    {
        private ICommunicationUI _communicationUI = communicationUI;

        static string _positiveAnswer = "y";
        static string _negativeAnswer = "n";

        static string _userEmptyFieldErrorMessage = "Пожалуйста, введите не пустую строку и не пробелы";
        static string _userNonPositiveNumberErrorMessage = "Пожалуйста, введите число большее 0";
        static string _userIncorrectAnswerErrorMessage = "Пожалуйста, введите либо 'y', либо 'n'";

        public bool GetAnswerInput( string label )
        {
            _communicationUI.WriteLine( label );
            return ReadAnswer();
        }

        public int GetPositiveIntInput( string label )
        {
            _communicationUI.WriteLine( label );
            return ReadPositiveInt();
        }

        public string GetStringInput( string label )
        {
            _communicationUI.WriteLine( label );
            return ReadNonEmptyString();
        }

        private int ReadPositiveInt()
        {
            string numStr = _communicationUI.ReadLine();
            int num = 0;
            while ( !int.TryParse( numStr, out num ) || num <= 0 )
            {
                _communicationUI.WriteLine( _userNonPositiveNumberErrorMessage );
                numStr = _communicationUI.ReadLine();
            }
            return num;
        }

        private string ReadNonEmptyString()
        {
            string input = _communicationUI.ReadLine();
            while ( string.IsNullOrWhiteSpace( input ) )
            {
                _communicationUI.WriteLine( _userEmptyFieldErrorMessage );
                input = _communicationUI.ReadLine();
            }
            return input;
        }

        private bool ReadAnswer()
        {
            string answer = _communicationUI.ReadLine();
            while ( answer.ToLower() != _positiveAnswer && answer.ToLower() != _negativeAnswer )
            {
                _communicationUI.WriteLine( _userIncorrectAnswerErrorMessage );
                answer = ReadNonEmptyString();
            }
            return answer.ToLower() == "y";
        }

    }
}
