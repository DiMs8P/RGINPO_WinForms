namespace RGINPO_WinForms;

public class Chart : Panel
{
    private Rectangle _drawingArea;

    private Rectangle _componentArea;

    public Chart()
    {
        _drawingArea = new Rectangle(new PointF(0, 0), new PointF(50, 50));

        _componentArea = new Rectangle(
                            new PointF(Location.X, Location.Y + Size.Height), 
                            new PointF(Location.X + Size.Width, Location.Y)
                         );

        MouseWheel += OnScroll_Handle;
    }
    
    public void OnScroll_Handle(object? sender, MouseEventArgs e)
    {

    }
}
