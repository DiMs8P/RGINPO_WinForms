using System.Windows.Forms.DataVisualization.Charting;

namespace RGINPO_WinForms
{
    public partial class Form1 : Form
    {
        public BindingSource BindingSource { get; set; }
        private CustomChart CustomChart { get; set; }

        public Form1()
        {
            BindingSource = new BindingSource();
            CustomChart = new CustomChart();

            InitializeComponent();
            CustomChart.InitChart();
            Controls.Add(CustomChart.chart1);

            comboBox1.Items.Add("Draw as line");
            comboBox1.Items.Add("Draw as spine");
            comboBox1.SelectedIndex = 0;

            dataGridView1.DataSource = BindingSource;
            BindingSource.ListChanged += comboBox1_SelectedIndexChanged;
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            BindingSource.Add(new Data() { X = 0, Y = 0 });
        }

        private void DrawAsLines()
        {
            CustomChart.chart1.DataSource = null;
            CustomChart.chart1.Series[0].ChartType = SeriesChartType.Line;
            CustomChart.chart1.DataSource = BindingSource;
        }
        private void DrawAsSpline()
        {
            CustomChart.chart1.DataSource = null;
            CustomChart.chart1.Series[0].ChartType = SeriesChartType.Spline;
            CustomChart.chart1.DataSource = BindingSource;
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

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                Filter = @"txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using var file = new StreamWriter(saveFileDialog1.FileName);
            for (var i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (var j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    file.Write(dataGridView1.Rows[i].Cells[j].Value + " ");
                }

                file.WriteLine("");
            }

            MessageBox.Show(@"File saved", @"Save successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Load_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (string fileName in openFileDialog.FileNames)
            {
                var fileContent = ReadFileContent(fileName);
                AddFileContentToDataGridView1(fileContent);
            }
        }

        private List<string> ReadFileContent(string filePath)
        {
            var fileContent = new List<string>();
            if (!File.Exists(filePath))
            {
                return fileContent;
            }

            using var reader = new StreamReader(filePath);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    fileContent.Add(line);
                }
            }

            return fileContent;
        }

        private void AddFileContentToDataGridView1(List<string> fileContent)
        {
            BindingSource.Clear();
            foreach (var values in fileContent.Select(line => line.Split(' ')))
            {
                var x = double.Parse(values[0]);
                var y = double.Parse(values[1]);

                BindingSource.Add(new Data() { X = x, Y = y });
            }
        }
    }
}
