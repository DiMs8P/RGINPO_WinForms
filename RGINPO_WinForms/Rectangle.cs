namespace RGINPO_WinForms;

public struct Rectangle
{
    public PointF LeftBottom { get; set; }
    public PointF RightTop { get; set; }

    public readonly float Width => RightTop.X - LeftBottom.X;

    public readonly float Height => RightTop.Y - LeftBottom.Y;

    public Rectangle(PointF pointLeftBottom, PointF pointRightTop)
    {
        LeftBottom = pointLeftBottom;
        RightTop = pointRightTop;
    }
}