namespace Accommodations.Models;

public class RoomCategory
{
    public string Name { get; init; }
    public decimal BaseRate { get; init; }
    public int AvailableRooms { get; set; }

    // Добавил Конструктор
    public RoomCategory( string name, decimal rate, int availRooms )
    {
        Name = name;
        BaseRate = rate;
        AvailableRooms = availRooms;
    }
}
