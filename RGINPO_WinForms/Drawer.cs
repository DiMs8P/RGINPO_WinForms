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

    public void DrawLines(Data[] points, Color color, Int32 borderWidth)
    {
        if (points is null || points.Length < 2) return;

        _drawer?.DrawLines(new Pen(color, borderWidth), TranslatePointsFromDrawAriaToApplicationArea(points));
    }

    public void DrawLine(Data point1, Data point2, Color color, Int32 borderWidth)
    {
        _drawer?.DrawLine(new Pen(color, borderWidth),
            UtilsLibrary.TranslatePointFromDrawAreaToApplicationArea(point1, _drawingArea, _componentArea),
            UtilsLibrary.TranslatePointFromDrawAreaToApplicationArea(point2, _drawingArea, _componentArea)
        );
    }

    public void DrawSpline(Data[] points, Color color, Int32 borderWidth)
    {
        if (points is null || points.Length < 3) return;

        _drawer?.DrawCurve(new Pen(color, borderWidth), TranslatePointsFromDrawAriaToApplicationArea(points));
    }

    public void DrawString(string value, Data point, Int32 size)
    {
        Font font = new(new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif), size);
        SolidBrush brush = new(Color.Black);

        Point pointTranslated = UtilsLibrary.TranslatePointFromDrawAreaToApplicationArea(point, _drawingArea, _componentArea);

        _drawer?.DrawString(value, font, brush, pointTranslated);
    }

    public void EndDraw()
    {
        _drawer?.Dispose();
    }

    private Point[] TranslatePointsFromDrawAriaToApplicationArea(Data[] points)
    {
        IList<Point> result = [];
        foreach (var point in points)
        {
            result.Add(UtilsLibrary.TranslatePointFromDrawAreaToApplicationArea(point, _drawingArea, _componentArea));
        }

        return [.. result];
    }
}