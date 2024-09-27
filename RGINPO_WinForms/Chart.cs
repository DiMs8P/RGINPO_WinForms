namespace RGINPO_WinForms;

public class Chart : Panel
{
    private Rectangle _drawingArea;

    private Rectangle _componentArea;
    private const int ScrollFactor = 10;

    private readonly Dictionary<Keys, Action<object?, KeyEventArgs>> _keyActions = new Dictionary<Keys, Action<object?, KeyEventArgs>>();

    public Chart()
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
        //int direction = e.Delta > 0 ? 1 : -1;
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
