﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RGINPO_WinForms;

public class Series2D : INotifyPropertyChanged
{
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
        if (ChartType is ChartType.Line)
        {
            drawer.DrawLines(Points, Color, BorderWidth);
        }
        else
        {
            drawer.DrawSpline(Points, Color, BorderWidth);
        }
    }
}