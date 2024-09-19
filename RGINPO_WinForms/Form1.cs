using System;
using System.ComponentModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace RGINPO_WinForms
{
    public partial class Form1 : Form
    {
        public BindingSource Data { get; set; }
        private CustomChart CustomChart { get; set; }
        
        public Form1()
        {
            Data = new BindingSource();
            CustomChart = new CustomChart();

            Data.Add(new Data() { X = 0, Y = 0 });
            Data.Add(new Data() { X = 1, Y = 1 });
            Data.Add(new Data() { X = 2, Y = 4 });
            InitializeComponent();
            CustomChart.InitChart();
            Controls.Add(CustomChart.chart1);

            comboBox1.Items.Add("Draw as line");
            comboBox1.Items.Add("Draw as spine");

            dataGridView1.DataSource = Data;
            Data.ListChanged += new ListChangedEventHandler(comboBox1_SelectedIndexChanged);
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            Data.Add(new Data() { X = 4, Y = 16 });
        }

        private void DrawAsLines()
        {
            CustomChart.chart1.DataSource = null;
            CustomChart.chart1.Series[0].ChartType = SeriesChartType.Line;
            CustomChart.chart1.DataSource = Data;
        }
        private void DrawAsSpline()
        {
            CustomChart.chart1.DataSource = null;
            CustomChart.chart1.Series[0].ChartType = SeriesChartType.Spline;
            CustomChart.chart1.DataSource = Data;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    DrawAsLines();
                    break;
                case 1:
                    DrawAsSpline();
                    break;
            }
        }
    }
}
