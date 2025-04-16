using CommunicationUI;
using InputValidationUI;
using InputValidationUI.Port;
using OrderManagingService;
using OrderManagingService.OrderManagingServicePort;

namespace OrderManager
{
    internal class App
    {
        private ICommunicationUI _communicationUI;
        private IInputValidationUI _inputValidationUI;
        private IOrderManagingService _orderManagingService;

        private static readonly string askForContinueMessage = "Желаете ли вы создать новый заказ? (Y / N)";

        public App( ICommunicationUI communicationUI )
        {
            _communicationUI = communicationUI;
            _inputValidationUI = new InputValidationUIPort( _communicationUI );
            _orderManagingService = new OrderManagingServicePort();
        }

        public void Run()
        {
            bool isRunning = true;
            while ( isRunning )
            {
                _orderManagingService.CreateOrderUseCase( _inputValidationUI, _communicationUI );

                isRunning = IsUserWantContinue();
            }
        }

        private bool IsUserWantContinue()
        {
            return _inputValidationUI.GetAnswerInput( askForContinueMessage );
        }
    }
}
