namespace RGINPO_WinForms;

public interface IAxis
{
    Rectangle Bounds { get; set; }

    public void Draw(Drawer drawer, double step);

    public IAxis SetBounds(Rectangle bounds);
}
