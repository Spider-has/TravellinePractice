namespace Accommodations.Models;

public class User
{
    public int Id { get; init; }
    public string Name { get; init; }

    // Добавил  Конструктор
    public User( int id, string name )
    {
        Id = id;
        Name = name;
    }
}
