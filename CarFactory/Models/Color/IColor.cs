
namespace CarFactory.Models.Color;

public interface IColor
{
    public PaintTypes PaintType { get; init; }

    public ColorTypes Color { get; init; }
}

public enum ColorTypes
{
    Black,
    Gray,
    White,
    Blue,
    Cyan,
    Red,
    Green,
    Yellow,
    Orange,
}

public enum PaintTypes
{
    Acrylic,
    Metallic,
    Chameleon,
    Matte,
    Chrome
}
