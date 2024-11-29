using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RGINPO_WinForms;

public class Series2D : INotifyPropertyChanged
{
    private delegate void DrawMethod(Data[] points, Color color, Int32 borderWidth);

    private Data[] _points = [];
    private Color _color = Color.Black;
    private int _borderWidth = 1;
    private ChartType _chartType;

    private readonly string _name;

    public Data[] Points
    {
        get => _points;
        set
        {
            _points = value;
            OnPropertyChanged(nameof(Points));
        }
    }

    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            OnPropertyChanged(nameof(Color));
        }
    }

    public int BorderWidth
    {
        get => _borderWidth;
        set
        {
            _borderWidth = value;
            OnPropertyChanged(nameof(BorderWidth));
        }
    }

    public ChartType ChartType
    {
        get => _chartType;
        set
        {
            _chartType = value;
            OnPropertyChanged(nameof(ChartType));
        }
    }

    public string Name => _name;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Series2D(string name)
    {
        _name = name;
    }

    public void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Draw(Drawer drawer)
    {
        Dictionary<ChartType, DrawMethod> drawDictionary = new()
        {
            { ChartType.Line, drawer.DrawLines},
            { ChartType.Spline, drawer.DrawSpline},
        };

        if (!drawDictionary.TryGetValue(ChartType, out var method))
        {
            throw new InvalidOperationException($"Не существует метода для рисования {ChartType}");
        }

        method?.Invoke(Points, Color, BorderWidth);
    }
}