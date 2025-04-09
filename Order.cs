struct OrderData
{
    public string ProductName;
    public int ProductCount;
    public string ClientName;
    public string DeliveryAddress;
    public OrderData()
    {
        ProductName = string.Empty;
        ProductCount = 0;
        ClientName = string.Empty;
        DeliveryAddress = string.Empty;
    }
}
class Order
{
    public OrderData orderData = new();
    public bool IsDataConfirmed { get; private set; }
    private DateTime _orderConfirmationTime;
    private static int _deliveryDayCount = 3;
    public void SetOrderConfirmed()
    {
        IsDataConfirmed = true;
        _orderConfirmationTime = DateTime.Now;
    }
    public void PrintOrderConfirmationMessage()
    {
        if ( IsDataConfirmed )
        {
            Console.WriteLine( $"{orderData.ClientName}! Ваш заказ {orderData.ProductName} в количестве {orderData.ProductCount} оформлен! Ожидайте доставку по адресу {orderData.DeliveryAddress} к {_orderConfirmationTime.AddDays( _deliveryDayCount )}" );
        }
        else
        {
            Console.WriteLine( "Похоже ваш заказ ещё не подтвержден, произошла какая-то ошибка, попробуйте пересоздать заказ" );
        }
    }

}

