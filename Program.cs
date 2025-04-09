﻿
public class Program
{
    public static void Main()
    {
        Console.WriteLine( "\nДобро пожаловать! Здесь вы можете создавать заказы!\n" );
        OrdersCreationProcess();
        Console.WriteLine( "\nСпасибо, что пользовались нашим приложением, до встречи!" );
    }

    static void OrdersCreationProcess()
    {
        bool isContinued = true;
        while ( isContinued )
        {
            Order order = new();
            ReadAllOrderData( ref order.orderData );
            if ( CheckOrderConfirmation( order.orderData ) )
            {
                order.SetOrderConfirmed();
                order.PrintOrderConfirmationMessage();
            }
            else
            {
                Console.WriteLine( "\nВ таком случае вы можете пересоздать заказ с измененными данными" );
            }
            isContinued = IsUserWantToRecreateOrder();
        }
    }
    static string ReadNonEmptyString()
    {
        string input = Console.ReadLine();
        while ( string.IsNullOrWhiteSpace( input ) )
        {
            Console.WriteLine( "Пожалуйста, введите не пустую строку и не пробелы" );
            input = Console.ReadLine();
        }
        return input;
    }
    static int ReadPositiveNumber()
    {
        string numStr = Console.ReadLine();
        int num = 0;
        while ( !int.TryParse( numStr, out num ) || num <= 0 )
        {
            Console.WriteLine( "Пожалуйста, введите число большее 0" );
            numStr = Console.ReadLine();
        }
        return num;
    }

    static bool ReadQuestionAnswer()
    {
        string answer = Console.ReadLine();
        while ( answer.ToLower() != "y" && answer.ToLower() != "n" )
        {
            Console.WriteLine( "Пожалуйста, введите либо 'y', либо 'n'" );
            answer = ReadNonEmptyString();
        }
        return answer.ToLower() == "y";
    }

    static void ReadAllOrderData( ref OrderData orderData )
    {
        Console.WriteLine( "\nВведите все параметры заказа.\n" );
        Console.Write( "Введите название продукта(название не может быть пустым или состоять из пробелов): " );
        orderData.ProductName = ReadNonEmptyString();
        Console.Write( "Введите количество продукта(количество должно быть больше 0): " );
        orderData.ProductCount = ReadPositiveNumber();
        Console.Write( "Введите ваше имя(имя не может быть пустым или состоять из пробелов): " );
        orderData.ClientName = ReadNonEmptyString();
        Console.Write( "Введите адрес доставки(адрес не может быть пустым или состоять из пробелов): " );
        orderData.DeliveryAddress = ReadNonEmptyString();
    }

    static bool CheckOrderConfirmation( OrderData orderData )
    {
        Console.WriteLine( $"\nВаше имя: {orderData.ClientName}, вы заказали {orderData.ProductCount} {orderData.ProductName} на адрес {orderData.DeliveryAddress}, всё верно? (Y / N) " );
        return ReadQuestionAnswer();
    }
    static bool IsUserWantToRecreateOrder()
    {
        Console.WriteLine( "\nЖелаете ли вы создать новый заказ? (Y / N)" );
        return ReadQuestionAnswer();
    }
}