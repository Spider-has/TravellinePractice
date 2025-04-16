using CommunicationUI;
using Domain.Order;
using InputValidationUI;

namespace OrderManagingService.OrderManagingServicePort
{
    internal class OrderManagingServicePort : IOrderManagingService
    {
        private static readonly string _productNameInputContent = "Введите название продукта(название не может быть пустым или состоять из пробелов): ";
        private static readonly string _productCountInputContent = "Введите количество продукта(количество должно быть больше 0): ";
        private static readonly string _clientNameInputContent = "Введите ваше имя(имя не может быть пустым или состоять из пробелов): ";
        private static readonly string _deliveryAdderessInputContent = "Введите адрес доставки(адрес не может быть пустым или состоять из пробелов): ";

        public void CreateOrderUseCase( IInputValidationUI inputValidationUI, ICommunicationUI communicationUI )
        {
            string productName = inputValidationUI.GetStringInput( _productNameInputContent );
            int productCount = inputValidationUI.GetPositiveIntInput( _productCountInputContent );
            string clientName = inputValidationUI.GetStringInput( _clientNameInputContent );
            string deliveryAddr = inputValidationUI.GetStringInput( _deliveryAdderessInputContent );

            bool isConfirmed = inputValidationUI.GetAnswerInput( GetAllUserDataConfirmMessage( productName, productCount, clientName, deliveryAddr ) );

            if ( isConfirmed )
            {
                Order userOrder = new( productName, productCount, clientName, deliveryAddr );

                communicationUI.WriteLine( GetSuccessOrderCreationMessage( userOrder.ProductName, userOrder.ProductCount, userOrder.ClientName, userOrder.DeliveryAddress, userOrder.OrderDeliveryDate ) );
            }
        }

        private static string GetAllUserDataConfirmMessage( string productName, int productCount, string clientName, string deliveryAddr )
        {
            return $"Ваше имя: {clientName}, вы заказали {productCount} {productName} на адрес {deliveryAddr}, всё верно? (Y / N) ";
        }

        private static string GetSuccessOrderCreationMessage( string productName, int productCount, string clientName, string deliveryAddr, DateTime deliveryDate )
        {
            return $"{clientName}! Ваш заказ {productCount} в количестве {productName} оформлен! Ожидайте доставку по адресу {deliveryAddr} к {deliveryDate}";
        }
    }
}
