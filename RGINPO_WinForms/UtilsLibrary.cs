namespace RGINPO_WinForms;

public class UtilsLibrary
{
    public static double RoundUpToPrecision(double number, double precision)
    {
        return Math.Ceiling(number / precision) * precision;
    }
    
    public static Point TranslatePointFromDrawAreaToApplicationArea(Data point, Rectangle drawingArea, Rectangle componentArea)
    {
        double coefficientX = (point.X - drawingArea.LeftBottom.X) / drawingArea.Width;
        double coefficientY = (point.Y - drawingArea.LeftBottom.Y) / drawingArea.Height;

        return new Point((int)(componentArea.LeftBottom.X + componentArea.Width * coefficientX), (int)(componentArea.RightTop.Y - componentArea.Height * (1 - coefficientY)));
    }

    public static Point TranslatePointFromApplicationAreaToDrawArea(Data point, Rectangle drawingArea, Rectangle componentArea)
    {
        return TranslatePointFromDrawAreaToApplicationArea(point, componentArea, drawingArea);
    }
}