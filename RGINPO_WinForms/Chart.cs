using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections;

namespace RGINPO_WinForms;

public class Chart : Panel
{
    public ObservableCollection<Series2D> Series = [];
    public event Action<Rectangle> OnDrawAreaChanged = null!;

    private const float MinSize = 0.001f;
    private const float MaxSize = 1000.0f;
    private const double MouseSensitivity = 0.01;
   
    private static readonly double[] Coefficients = [2, 2.5, 2];

    private int _scaleIndex = 2;
    private double _drawStep = 5;

    private Rectangle _drawingArea;
    private Rectangle _componentArea;
    private Point _lastMousePosition;

    private readonly XAxis _xAxis = new();
    private readonly YAxis _yAxis = new();
    private readonly Drawer _drawer = new();

    public Rectangle DrawingArea => _drawingArea;
    public Rectangle ComponentArea => _componentArea;
    private int ScaleIndex
    {
        get => _scaleIndex;
        set
        {
            if (value >= Coefficients.Length)
            {
                _scaleIndex = 0;
            } 
            else if (value < 0)
            {
                _scaleIndex = Coefficients.Length - 1;
            }
            else
            {
                _scaleIndex = value;
            }
        }
    }

    public void Initialize()
    {
        InitializeAreas();
        BindToComponentEvents();
        BindToCustomEvents();
    }

    private void InitializeAreas()
    {
        _drawingArea = new Rectangle(new Data(0, 0), new Data(50, 50));

        _componentArea = new Rectangle(
            new Data(0, Size.Height),
            new Data(Size.Width, 0)
        );

        OnDrawAreaChanged?.Invoke(_drawingArea);
    }

    private void BindToComponentEvents()
    {
        MouseWheel += OnScroll_Handle;
        MouseMove += OnMouseMove_Handle;
        Paint += OnPaint_Handle;
    }

    private void OnPaint_Handle(object? sender, PaintEventArgs e)
    {
        _xAxis.UpdateBounds(DrawingArea);
        _yAxis.UpdateBounds(DrawingArea);

        _drawer.BeginDraw(e.Graphics, DrawingArea, ComponentArea);

        foreach (var series in Series)
        {
            series.Draw(_drawer);
        }

        _xAxis.Draw(_drawer, _drawStep);
        _yAxis.Draw(_drawer, _drawStep);

        _drawer.EndDraw();
    }

    private void BindToCustomEvents()
    {
        OnDrawAreaChanged += (Rectangle r) => Invalidate();

        Series.CollectionChanged += OnCollectionChanged_Handle;
    }

    private void OnScroll_Handle(object? sender, MouseEventArgs e)
    {
        int direction = e.Delta > 0 ? 1 : -1;
        if (direction > 0 && (_drawingArea.Height < MinSize || _drawingArea.Width < MinSize))
        {
            return;
        }

        if (direction < 0 && (_drawingArea.Height > MaxSize || _drawingArea.Width > MaxSize))
        {
            return;
        }

        Point inputPoint = e.Location;

        double leftLength = inputPoint.X - _componentArea.LeftBottom.X;
        double rightLength = _componentArea.RightTop.X - inputPoint.X;
        double bottomLength = _componentArea.LeftBottom.Y - inputPoint.Y;
        double topLength = inputPoint.Y - _componentArea.RightTop.Y;

        double leftCoefficient = leftLength / _componentArea.Width;
        double rightCoefficient = rightLength / _componentArea.Width;
        double bottomCoefficient = bottomLength / -_componentArea.Height;
        double topCoefficient = topLength / -_componentArea.Height;
        double scrollFactor = (_drawingArea.RightTop.X - _drawingArea.LeftBottom.X) / 10.0f;
        Data leftPoint = new(_drawingArea.LeftBottom.X + direction * leftCoefficient * scrollFactor,
                               _drawingArea.LeftBottom.Y + direction * bottomCoefficient * scrollFactor);

        Data rightPoint = new(_drawingArea.RightTop.X - direction * rightCoefficient * scrollFactor,
                                _drawingArea.RightTop.Y - direction * topCoefficient * scrollFactor);

        _drawingArea.LeftBottom = leftPoint;
        _drawingArea.RightTop = rightPoint;

        UpdateDrawStep();
        OnDrawAreaChanged?.Invoke(_drawingArea);
    }

    private void UpdateDrawStep()
    {
        int numOfSegments = (int)(_drawingArea.Width / _drawStep) + 1;
        if (numOfSegments > 20)
        {
            ++ScaleIndex;
            _drawStep *= Coefficients[ScaleIndex];
        }
        else if (numOfSegments < 7)
        {
            --ScaleIndex;
            _drawStep /= Coefficients[ScaleIndex];
        }
    }
    
    private void OnMouseMove_Handle(object? sender, MouseEventArgs e)
    {
        if (MouseButtons.HasFlag(MouseButtons.Left))
        {
            var dx = _lastMousePosition.X - e.X;
            var dy = e.Y - _lastMousePosition.Y;

            Data offset = new(dx, dy);

            _drawingArea.LeftBottom += offset * _drawStep * MouseSensitivity;
            _drawingArea.RightTop += offset * _drawStep * MouseSensitivity;
            
            OnDrawAreaChanged?.Invoke(_drawingArea);
        }
        _lastMousePosition = e.Location;
    }
    private void OnCollectionChanged_Handle(object? sender, NotifyCollectionChangedEventArgs e)
    {
        var subscribe = (IList? items) =>
        {
            if (items is null) return;

            foreach (var item in items)
            {
                if (item is Series2D series)
                {
                    series.PropertyChanged += OnRenderRequest_Handle;
                }
            }
        };

        var unSubscribe = (IList? items) =>
        {
            if (items is null) return;

            foreach (var item in items)
            {
                if (item is Series2D series)
                {
                    series.PropertyChanged -= OnRenderRequest_Handle;
                }
            }
        };

        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add: subscribe(e.NewItems); break;
            case NotifyCollectionChangedAction.Remove: unSubscribe(e.OldItems); break;
            case NotifyCollectionChangedAction.Replace: unSubscribe(e.OldItems); subscribe(e.NewItems); break;
            case NotifyCollectionChangedAction.Reset: unSubscribe(e.OldItems); break;
        }

        Invalidate();
    }

    private void OnRenderRequest_Handle(object? sender, PropertyChangedEventArgs args) => Invalidate();
}
