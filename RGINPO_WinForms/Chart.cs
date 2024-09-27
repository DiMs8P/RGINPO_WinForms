namespace RGINPO_WinForms;

public class Chart : Panel
{
    private const int ScrollFactor = 10;
    
    private Rectangle _drawingArea;
    private Rectangle _componentArea;

    private readonly Dictionary<Keys, Action<object?, KeyEventArgs>> _keyActions = [];

    public event Action<Rectangle> OnDrawAreaChanged = null!;

    public Rectangle DrawingArea => _drawingArea;
    public Rectangle ComponentArea => _componentArea;

    public Chart()
    {

    }

    public void Initialize()
    {
        InitializeAreas();
        BindToComponentEvents();
        BindToCustomEvents();
    }

    private void InitializeAreas()
    {
        _drawingArea = new Rectangle(new PointF(0, 0), new PointF(50, 50));

        _componentArea = new Rectangle(
            new PointF(Location.X, Location.Y + Size.Height), 
            new PointF(Location.X + Size.Width, Location.Y)
        );

        OnDrawAreaChanged?.Invoke(_drawingArea);
    }
    
    private void BindToComponentEvents()
    {
        MouseWheel += OnScroll_Handle;
        KeyDown += OnKeyDown_Handle;
    }

    private void BindToCustomEvents()
    {
        _keyActions.Add(Keys.F, OnZoom_Handle);
    }

    private void OnScroll_Handle(object? sender, MouseEventArgs e)
    {
        int direction = e.Delta > 0 ? 1 : -1;

        Point inputPoint = e.Location;

        float leftLength = inputPoint.X - _componentArea.LeftBottom.X;
        float rightLength = _componentArea.RightTop.X - inputPoint.X;
        float bottomLength = _componentArea.LeftBottom.Y - inputPoint.Y;
        float topLength = inputPoint.Y - _componentArea.RightTop.Y;

        float leftCoefficient = leftLength / _componentArea.Width;
        float rightCoefficient = rightLength / _componentArea.Width;
        float bottomCoefficient = bottomLength / _componentArea.Height;
        float topCoefficient = topLength / _componentArea.Height;


        PointF leftPoint = new(_drawingArea.LeftBottom.X + direction * leftCoefficient * ScrollFactor,
                               _drawingArea.LeftBottom.Y + direction * bottomCoefficient * ScrollFactor);

        PointF rightPoint = new(_drawingArea.RightTop.X - direction * rightCoefficient * ScrollFactor,
                                _drawingArea.RightTop.Y - direction * topCoefficient * ScrollFactor);

        _drawingArea.LeftBottom = leftPoint;
        _drawingArea.RightTop = rightPoint;

        OnDrawAreaChanged(_drawingArea);
    }
    
    private void OnKeyDown_Handle(object? sender, KeyEventArgs e)
    {
        if (_keyActions.TryGetValue(e.KeyCode, out var action))
        {
            action(sender, e);
        }
    }
    
    private void OnZoom_Handle(object? sender, KeyEventArgs e)
    {
        
    }
}
