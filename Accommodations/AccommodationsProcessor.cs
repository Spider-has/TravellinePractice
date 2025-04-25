using System.Globalization;
using Accommodations.Commands;
using Accommodations.Dto;
using Accommodations.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Accommodations;

public static class AccommodationsProcessor
{
    private static BookingService _bookingService = new();
    private static Dictionary<int, ICommand> _executedCommands = new();
    private static int s_commandIndex = 0;

    public static void Run()
    {
        Console.WriteLine( "Booking Command Line Interface" );
        Console.WriteLine( "Commands:" );
        Console.WriteLine( "'book <UserId> <Category> <StartDate> <EndDate> <Currency>' - to book a room" );
        Console.WriteLine( "'cancel <BookingId>' - to cancel a booking" );
        Console.WriteLine( "'undo' - to undo the last command" );
        Console.WriteLine( "'find <BookingId>' - to find a booking by ID" );
        Console.WriteLine( "'search <StartDate> <EndDate> <CategoryName>' - to search bookings" );
        Console.WriteLine( "'exit' - to exit the application" );

        string input;
        while ( ( input = Console.ReadLine() ) != "exit" )
        {
            try
            {
                ProcessCommand( input );
            }
            catch ( ArgumentException ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
        }
    }

    private static void ProcessCommand( string input )
    {
        string[] parts = input.Split( ' ' );
        string commandName = parts[ 0 ];

        switch ( commandName )
        {
            case "book":
                if ( parts.Length != 6 )
                {
                    Console.WriteLine( "Invalid number of arguments for booking." );
                    return;
                }

                // Добавил все проверки на валидность полей
                if ( !int.TryParse( parts[ 1 ], out int userId ) )
                {
                    throw new ArgumentException( "Error pasring user id, invalid userId input" );
                }

                // Явно выбираю культуру для даты ( у меня не работали примеры из github без этого )
                if ( !DateTime.TryParse( parts[ 3 ], CultureInfo.CreateSpecificCulture( "en-US" ), out DateTime startD ) )
                {
                    throw new ArgumentException( "Error pasring start date, invalid date input, please try mm/dd/yyyy" );
                }

                // Явно выбираю культуру для даты ( у меня не работали примеры из github без этого )
                if ( !DateTime.TryParse( parts[ 4 ], CultureInfo.CreateSpecificCulture( "en-US" ), out DateTime endD ) )
                {
                    throw new ArgumentException( "Error pasring end date, invalid date input, please try mm/dd/yyyy" );
                }

                if ( !Enum.TryParse( typeof( CurrencyDto ), parts[ 5 ], true, out object? currencyDto ) )
                {
                    throw new ArgumentException( "Error pasring currency, such currency did not found" );
                }

                BookingDto bookingDto = new( userId, parts[ 2 ], startD, endD, ( CurrencyDto )currencyDto );

                BookCommand bookCommand = new( _bookingService, bookingDto );
                bookCommand.Execute();
                _executedCommands.Add( ++s_commandIndex, bookCommand );
                Console.WriteLine( "Booking command run is successful." );
                break;

            case "cancel":
                if ( parts.Length != 2 )
                {
                    Console.WriteLine( "Invalid number of arguments for canceling." );
                    return;
                }
                // Проверка на корректный guid
                if ( !Guid.TryParse( parts[ 1 ], out Guid bookingId ) )
                {
                    throw new ArgumentException( "Error parsing booking id, please enter it in Guid format" );
                }
                CancelBookingCommand cancelCommand = new( _bookingService, bookingId );
                cancelCommand.Execute();
                _executedCommands.Add( ++s_commandIndex, cancelCommand );
                Console.WriteLine( "Cancellation command run is successful." );
                break;

            case "undo":
                // Поправил undo, теперь нельзя отменить действие, если оно не находится в executedCommands
                if ( _executedCommands.ContainsKey( s_commandIndex ) )
                {
                    _executedCommands[ s_commandIndex ].Undo();
                    _executedCommands.Remove( s_commandIndex );
                    s_commandIndex--;
                    Console.WriteLine( "Last command undone." );
                }
                else
                {
                    Console.WriteLine( "Cannot find such command index to undo " );
                }
                break;
            case "find":
                if ( parts.Length != 2 )
                {
                    Console.WriteLine( "Invalid arguments for 'find'. Expected format: 'find <BookingId>'" );
                    return;
                }
                // Проверка на корректный guid
                if ( !Guid.TryParse( parts[ 1 ], out Guid id ) )
                {
                    throw new ArgumentException( "Error parsing booking id, please enter it in Guid format" );
                }
                FindBookingByIdCommand findCommand = new( _bookingService, id );
                findCommand.Execute();
                break;

            case "search":
                if ( parts.Length != 4 )
                {
                    Console.WriteLine( "Invalid arguments for 'search'. Expected format: 'search <StartDate> <EndDate> <CategoryName>'" );
                    return;
                }

                // Явно выбираю культуру для даты ( у меня не работали примеры из github без этого )
                if ( !DateTime.TryParse( parts[ 1 ], CultureInfo.CreateSpecificCulture( "en-US" ), out DateTime startDate ) )
                {
                    throw new ArgumentException( "Error pasring start date, invalid date input, please try mm/dd/yyyy" );
                }

                // Явно выбираю культуру для даты ( у меня не работали примеры из github без этого )
                if ( !DateTime.TryParse( parts[ 2 ], CultureInfo.CreateSpecificCulture( "en-US" ), out DateTime endDate ) )
                {
                    throw new ArgumentException( "Error pasring end date, invalid date input, please try mm/dd/yyyy" );
                }
                string categoryName = parts[ 3 ];
                SearchBookingsCommand searchCommand = new( _bookingService, startDate, endDate, categoryName );
                searchCommand.Execute();
                break;

            default:
                Console.WriteLine( "Unknown command." );
                break;
        }
    }
}
