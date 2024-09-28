namespace RGINPO_WinForms;

public struct Rectangle
{
    public Data LeftBottom { get; set; }
    public Data RightTop { get; set; }

    public readonly double Width => RightTop.X - LeftBottom.X;

    public readonly double Height => RightTop.Y - LeftBottom.Y;

    public Rectangle(Data pointLeftBottom, Data pointRightTop)
    {
        LeftBottom = pointLeftBottom;
        RightTop = pointRightTop;
    }
}