
namespace CarFactory.Utils;
public static class EnumsToList
{
    public static List<T> EnumToList<T>()
    {
        return Enum.GetValues( typeof( T ) ).Cast<T>().ToList();
    }
}
