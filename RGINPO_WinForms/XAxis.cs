namespace RGINPO_WinForms;

public class XAxis : IAxis
{ 
    public Rectangle Bounds { get; set ; }

    public void Draw(Drawer drawer, double step)
    {
        double xCurrent = Bounds.LeftBottom.X;
        int i = 1;

        while (xCurrent < Bounds.RightTop.X) 
        {
            Data point1 = new(xCurrent, Bounds.RightTop.Y);
            Data point2 = new(xCurrent, Bounds.LeftBottom.Y);

            drawer.DrawLine(point1, point2, Color.FromArgb(128, Color.Black), 1);

            xCurrent = Bounds.LeftBottom.X + i * step;
            ++i;
        }
    }

    public IAxis SetBounds(Rectangle bounds)
    {
        Bounds = bounds;

        return this;
    }
}
