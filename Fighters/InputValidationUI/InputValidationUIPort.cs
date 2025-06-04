
using Fighters.CommunicationUI;

namespace Fighters.InputValidationUI;

internal class InputValidationUIPort( ICommunicationUI communicationUI) : IInputValidationUI
{
    private ICommunicationUI _communicationUI = communicationUI;
    static string _positiveAnswer = "y";
    static string _negativeAnswer = "n";

    static string _userEmptyFieldErrorMessage = "Пожалуйста, введите не пустую строку и не пробелы";
    static string _userIncorrectOptionErrorMessage = "Похоже вы выбрали несуществующий вариант";
    static string _userIncorrectAnswerErrorMessage = "Пожалуйста, введите либо 'y', либо 'n'";

    public bool GetAnswerInput( string label )
    {
        _communicationUI.WriteLine( label );
        return ReadAnswer();
    }

    public int GetOptionsInput( string label, List<string> options )
    {
        _communicationUI.WriteLine( label );
        for ( int i = 0; i < options.Count; i++ )
        {
            _communicationUI.WriteLine( $"  ({i + 1}). {options[ i ]} ");
        }
        return ReadPositiveLimitedInt( options.Count ) - 1;
    }

    public string GetStringInput( string label )
    {
        _communicationUI.WriteLine( label );
        return ReadNonEmptyString();
    }

    private int ReadPositiveLimitedInt( int limit )
    {
        string numStr = _communicationUI.ReadLine();
        int num;
        while ( !int.TryParse( numStr, out num ) || num <= 0 || num > limit )
        {
            _communicationUI.WriteLine( _userIncorrectOptionErrorMessage );
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
