namespace RGINPO_WinForms;

public class YAxis : IAxis
{
    public Rectangle Bounds { get; set ; }

    public void Draw(Drawer drawer, double step)
    {
        double yCurrent = Bounds.LeftBottom.Y;
        int i = 1;

        while (yCurrent < Bounds.RightTop.Y)
        {
            Data point1 = new(Bounds.LeftBottom.X, yCurrent);
            Data point2 = new(Bounds.RightTop.X, yCurrent);

            drawer.DrawLine(point1, point2, Color.FromArgb(128, Color.Black), 1);

            yCurrent = Bounds.LeftBottom.Y + i * step;
            ++i;
        }
    }

    public IAxis SetBounds(Rectangle bounds)
    {
        Bounds = bounds;

        return this;
    }
}
