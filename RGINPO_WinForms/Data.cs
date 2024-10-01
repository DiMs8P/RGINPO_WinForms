namespace RGINPO_WinForms;

public class Data
{
    public double X { get; set; }
    public double Y { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Использовать основной конструктор", Justification = "<Ожидание>")]
    public Data(double x, double y)
    {
        X = x;
        Y = y;
    }
    
    public static Data operator +(Data data1, Data data2)
    {
        return new Data(data1.X + data2.X, data1.Y + data2.Y);
    }
    
    public static Data operator *(Data data1, double factor)
    {
        return new Data(data1.X * factor, data1.Y * factor);
    }
};