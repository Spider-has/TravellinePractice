namespace Accommodations.Models;

public class Booking
{
    public Guid Id { get; init; }
    public int UserId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public RoomCategory RoomCategory { get; init; }
    public Currency Currency { get; init; }
    public decimal Cost { get; init; }


    //Добавил конструктор и приватные методы проверки дат
    public Booking( int userId, DateTime startDate, DateTime endDate, RoomCategory category, Currency currency, decimal cost )
    {
        Id = Guid.NewGuid();
        UserId = userId;
        if ( !IsStartDateCorrect( startDate ) )
        {
            throw new ArgumentException( "Start date cannot be earlier than today date" );
        }
        StartDate = startDate;
        if ( !IsEndDateCorrect( startDate, endDate ) )
        {
            throw new ArgumentException( "End date cannot be earlier than start date" );
        }
        EndDate = endDate;
        RoomCategory = category;
        Currency = currency;
        Cost = cost;
    }

    private static bool IsStartDateCorrect( DateTime startDate )
    {
        return startDate.Date >= DateTime.Now.Date;
    }

    private bool IsEndDateCorrect( DateTime startDate, DateTime endDate )
    {
        return startDate < endDate;
    }

}
