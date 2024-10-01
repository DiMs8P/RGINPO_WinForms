using System.Globalization;

namespace RGINPO_WinForms;

public class YAxis : IAxis
{
    public Rectangle Bounds { get; set ; }

    public void Draw(Drawer drawer, double step)
    {
        double start = UtilsLibrary.RoundUpToPrecision(Bounds.LeftBottom.Y, step);
        double yCurrent = start;
        
        int i = 1;
        while (yCurrent < Bounds.RightTop.Y)
        {
            Data point1 = new(Bounds.LeftBottom.X, yCurrent);
            Data point2 = new(Bounds.RightTop.X, yCurrent);
            Data point3 = new(Bounds.LeftBottom.X, yCurrent);

            drawer.DrawLine(point1, point2, Color.FromArgb(128, Color.Black), 1);
            drawer.DrawString(point3.Y.ToString("0.####"), point3, 11);

            yCurrent = start + i * step;
            ++i;
        }
    }

    public IAxis UpdateBounds(Rectangle bounds)
    {
        Bounds = bounds;

        return this;
    }
}
