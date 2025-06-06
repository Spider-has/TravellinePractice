namespace CarFactory.Models.Color;

internal class CarColor( PaintTypes paintType, ColorTypes colorType ) : IColor
{
    public PaintTypes PaintType { get; init; } = paintType;
    public ColorTypes Color { get; init; } = colorType;

}
