namespace InputValidationUI
{
    internal interface IInputValidationUI
    {
        public string GetStringInput( string label );

        public int GetPositiveIntInput( string label );

        public bool GetAnswerInput( string label );
    }
}
