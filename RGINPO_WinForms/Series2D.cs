using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RGINPO_WinForms;

public class Series2D : INotifyPropertyChanged
{
    private Data[] _points = [];
    private Color _color = Color.Black;
    private int _borderWidth = 1;

    private readonly string _name;
    
    public Series2D this[string name] => this[name];

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
        drawer.DrawLines(Points, Color, BorderWidth);
    }
}