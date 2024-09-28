namespace RGINPO_WinForms;

public class Drawer
{
    private Graphics? _drawer;
    private Rectangle _drawingArea;
    private Rectangle _componentArea;

    public void BeginDraw(Graphics g, Rectangle drawingArea, Rectangle componentArea)
    {
        _drawer = g;
        _drawingArea = drawingArea;
        _componentArea = componentArea;
    }

    public void DrawLines(PointF[] points, Color color, Int32 borderWidth)
    {
        if (points.Length < 2) return;

        _drawer?.DrawLines(new Pen(color, borderWidth), TranslatePointsFromDrawAriaToApplicationArea(points));
    }

    public void EndDraw()
    {
        _drawer = null;
        // _drawer?.Dispose();
    }

    private Point TranslatePointFromDrawAriaToApplicationArea(PointF point)
    {
        float coefficientX = (point.X - _drawingArea.LeftBottom.X) / _drawingArea.Width;
        float coefficientY = (point.Y - _drawingArea.LeftBottom.Y) / _drawingArea.Height;

        return new Point((int)(_componentArea.LeftBottom.X + _componentArea.Width * coefficientX), (int)(_componentArea.RightTop.Y - _componentArea.Height * (1 - coefficientY)));
    }

    public Point[] TranslatePointsFromDrawAriaToApplicationArea(PointF[] points)
    {
        IList<Point> result = [];
        
        foreach (var point in points)
        {
            result.Add(TranslatePointFromDrawAriaToApplicationArea(point));
        }

        return [.. result];
    }
}