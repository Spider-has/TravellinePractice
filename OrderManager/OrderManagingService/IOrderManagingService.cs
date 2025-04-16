using CommunicationUI;
using InputValidationUI;

namespace OrderManagingService
{
    internal interface IOrderManagingService
    {
        public void CreateOrderUseCase( IInputValidationUI inputValidationUI, ICommunicationUI communicationUI );
    }
}
