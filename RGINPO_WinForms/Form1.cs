using System.Windows.Forms;
using System;

namespace RGINPO_WinForms
{
    public partial class Form1 : Form
    {
        public BindingSource Data { get; set; }
        public Form1()
        {
            Data = new BindingSource();
            Data.Add(new Data() { X = 0, Y = 0 });
            Data.Add(new Data() { X = 1, Y = 1 });
            Data.Add(new Data() { X = 2, Y = 4 });
            InitializeComponent();
            dataGridView1.DataSource = Data;
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            Data.Add(new Data() { X = 4, Y = 16 });
        }
        private void DrawAsLines_Click(object sender, EventArgs e)
        {
            chart1.DataSource = null;
            chart1.Series[0].ChartType =
           System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.DataSource = Data;
        }
        private void DrawAsSpline_Click(object sender, EventArgs e)
        {
            chart1.DataSource = null;
            chart1.Series[0].ChartType =
           System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.DataSource = Data;
        }
    }
    public class Data
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
