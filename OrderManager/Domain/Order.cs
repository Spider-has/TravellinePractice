namespace Domain.Order
{
    class Order
    {
        public string ProductName { get; private init; }
        public int ProductCount { get; private init; }
        public string ClientName { get; private init; }
        public string DeliveryAddress { get; private init; }
        public DateTime OrderConfirmationTime { get; private init; }
        public DateTime OrderDeliveryDate { get; private set; }

        private static readonly int _deliveryDayCount = 3;

        public Order( string prName, int prCount, string clName, string delAddress )
        {
            ProductName = prName;
            ProductCount = prCount;
            ClientName = clName;
            DeliveryAddress = delAddress;
            OrderConfirmationTime = DateTime.Now;

            SetDelivaryDate( OrderConfirmationTime.AddDays( _deliveryDayCount ) );
        }

        private void SetDelivaryDate( DateTime deliveryDate )
        {
            if ( deliveryDate > DateTime.Now )
                OrderDeliveryDate = deliveryDate;
        }

    }


}
