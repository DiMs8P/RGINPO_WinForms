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
};