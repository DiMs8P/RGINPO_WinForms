using System.Windows.Forms.DataVisualization.Charting;

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
            InitKALL();

            comboBox1.Items.Add("Draw as line");
            comboBox1.Items.Add("Draw as spine");

            dataGridView1.DataSource = Data;
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            Data.Add(new Data() { X = 4, Y = 16 });
        }

        private void DrawAsLines_Click()
        {
            chart1.DataSource = null;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.DataSource = Data;
        }
        private void DrawAsSpline_Click()
        {
            chart1.DataSource = null;
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.DataSource = Data;
        }


        private ChartArea chartArea1 = new();
        private Legend legend1 = new();
        private Series series1 = new();
        private Chart chart1 = new();

        private void InitKALL()
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

            this.Controls.Add(chart1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    DrawAsLines_Click();
                    break;
                case 1:
                    DrawAsSpline_Click();
                    break;
            }
        }
    }
}
