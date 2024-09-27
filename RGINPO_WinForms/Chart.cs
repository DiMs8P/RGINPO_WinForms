namespace RGINPO_WinForms;

public class Chart : Panel
{
    private const float MinSize = 0.1f;
    private const float MaxSize = 100.0f;
    
    private Rectangle _drawingArea;
    private Rectangle _componentArea;

    private readonly Dictionary<Keys, Action<object?, KeyEventArgs>> _keyActions = [];

    public event Action<Rectangle> OnDrawAreaChanged = null!;

    public Rectangle DrawingArea => _drawingArea;
    public Rectangle ComponentArea => _componentArea;

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
            new PointF(0, Size.Height), 
            new PointF(Size.Width, 0)
        );

        OnDrawAreaChanged?.Invoke(_drawingArea);
    }
    
    private void BindToComponentEvents()
    {
        MouseWheel += OnScroll_Handle;
        KeyDown += OnKeyDown_Handle;
        Paint += OnPaint_Handle;
    }

    private void OnPaint_Handle(object? sender, PaintEventArgs e)
    {
        /*PointF leftCenterPoint = new PointF(0, 25);
        PointF rightCenterPoint = new PointF(50, 25);
        
        Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
        e.Graphics.DrawLine(pen, TranslatePointFromDrawAriaToApplicationArea(leftCenterPoint), TranslatePointFromDrawAriaToApplicationArea(rightCenterPoint));*/
    }

    private void BindToCustomEvents()
    {
        _keyActions.Add(Keys.F, OnFocus_Handle);
        OnDrawAreaChanged += (Rectangle r) => Invalidate();
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

        float leftLength = inputPoint.X - _componentArea.LeftBottom.X;
        float rightLength = _componentArea.RightTop.X - inputPoint.X;
        float bottomLength = _componentArea.LeftBottom.Y - inputPoint.Y;
        float topLength = inputPoint.Y - _componentArea.RightTop.Y;

        float leftCoefficient = leftLength / _componentArea.Width;
        float rightCoefficient = rightLength / _componentArea.Width;
        float bottomCoefficient = bottomLength / -_componentArea.Height;
        float topCoefficient = topLength / -_componentArea.Height;

        float scrollFactor = (_drawingArea.RightTop.X - _drawingArea.LeftBottom.X) / 10.0f;
        PointF leftPoint = new(_drawingArea.LeftBottom.X + direction * leftCoefficient * scrollFactor,
                               _drawingArea.LeftBottom.Y + direction * bottomCoefficient * scrollFactor);

        PointF rightPoint = new(_drawingArea.RightTop.X - direction * rightCoefficient * scrollFactor,
                                _drawingArea.RightTop.Y - direction * topCoefficient * scrollFactor);

        _drawingArea.LeftBottom = leftPoint;
        _drawingArea.RightTop = rightPoint;

        OnDrawAreaChanged?.Invoke(_drawingArea);
    }
    
    private void OnKeyDown_Handle(object? sender, KeyEventArgs e)
    {
        if (_keyActions.TryGetValue(e.KeyCode, out var action))
        {
            action(sender, e);
        }
    }
    
    private void OnFocus_Handle(object? sender, KeyEventArgs e)
    {
        
    }

    private Point TranslatePointFromDrawAriaToApplicationArea(PointF point)
    {
        float coefficientX = (point.X - _drawingArea.LeftBottom.X) / _drawingArea.Width;
        float coefficientY = (point.Y - _drawingArea.LeftBottom.Y) / _drawingArea.Height;
        
        return new Point((int)(_componentArea.LeftBottom.X + _componentArea.Width * coefficientX), (int)(_componentArea.RightTop.Y - _componentArea.Height * (1 - coefficientY)));
    }
}
