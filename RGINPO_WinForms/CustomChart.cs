using System.Windows.Forms.DataVisualization.Charting;

namespace RGINPO_WinForms;

public class CustomChart
{
    public ChartArea chartArea1 = new();
    public Legend legend1 = new();
    public Series series1 = new();
    public Chart chart1 = new();

    public void InitChart()
    {
        chartArea1.Name = "ChartArea1";
        chart1.ChartAreas.Add(chartArea1);
        legend1.Enabled = false;
        legend1.Name = "Legend1";
        chart1.Legends.Add(legend1);
        chart1.Location = new Point(12, 16);
        chart1.Name = "chart1";
        series1.ChartArea = "ChartArea1";
        series1.Legend = "Legend1";
        series1.Name = "Series1";
        series1.XValueMember = "X";
        series1.YValueMembers = "Y";
        chart1.Series.Add(series1);
        chart1.Size = new Size(300, 300);
        chart1.TabIndex = 2;
        chart1.Text = "chart1";
    }
}
