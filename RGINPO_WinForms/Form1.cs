using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace RGINPO_WinForms;

public partial class Form1 : Form
{
    private readonly Dictionary<string, string> _files = [];
    private readonly Dictionary<int, SeriesChartType> DrawMode = new()
    {
        { 0,SeriesChartType.Line},
        { 1, SeriesChartType.Spline},
        {-1, SeriesChartType.ErrorBar},
    };
    public BindingSource BindingSourceData { get; set; } = [];

    void InitChart()
    {
        ChartArea chartArea1 = new();
        Legend legend1 = new();
        Series series1 = new();

        chartArea1.Name = "ChartArea1";
        legend1.Enabled = true;
        legend1.Name = "Legend1";
        series1.ChartArea = "ChartArea1";
        series1.Legend = "Legend1";
        series1.Name = "Default";
        series1.XValueMember = "X";
        series1.YValueMembers = "Y";

        chart1.ChartAreas.Add(chartArea1);
        chart1.Legends.Add(legend1);
        chart1.Series.Add(series1);
    }

    public Form1()
    {
        InitializeComponent();
        InitChart();
        comboBox1.SelectedIndex = 0;

        dataGridView1.DataSource = BindingSourceData;
        BindingSourceData.ListChanged += ComboBox1_SelectedIndexChanged;

        DataGridViewTextBoxColumn fileNameColumn = new()
        {
            HeaderText = "File name"
        };
        dataGridView2.Columns.Add(fileNameColumn);
        dataGridView2.CellContentClick += DataGridView2_CellContentClick;
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        BindingSourceData.Add(new Data(0, 0));
    }

    private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        chart1.DataSource = null;
        
        //chart1.Series[0].ChartType = DrawMode[comboBox1.SelectedIndex];

        foreach (Series item in chart1.Series)
        {
            item.ChartType = DrawMode[comboBox1.SelectedIndex];
        }

        chart1.DataSource = BindingSourceData;
    }

    private void Save_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog1 = new()
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
        for (var i = 0; i < dataGridView1.Rows.Count; i++)
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
        using OpenFileDialog openFileDialog = new()
        {
            Multiselect = true,
        };
        if (openFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        foreach (string fullFileName in openFileDialog.FileNames)
        {
            string directoryPath = Path.GetDirectoryName(fullFileName)!;
            string fileName = Path.GetFileName(fullFileName);
            _files.Add(fileName, directoryPath);

            dataGridView2.Rows.Add(fileName);
            var fileContent = ReadFileContent(fullFileName);
            AddFileContentToDataGridView1(fileContent);
            CreateChartSeries(BindingSourceData, fileName);
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

    private void AddFileContentToDataGridView1(IList<string> fileContent)
    {
        BindingSourceData.Clear();
        foreach (var values in fileContent.Select(line => line.Split(' ')))
        {
            var x = double.Parse(values[0]);
            var y = double.Parse(values[1]);

            BindingSourceData.Add(new Data(x, y));
        }
    }

    private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && e.RowIndex < dataGridView2.Rows.Count)
        {
            dataGridView1.Rows.Clear();

            string fileName = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            string filePath = Path.Combine(_files[fileName], fileName);

            List<string> fileContent = ReadFileContent(filePath);
            AddFileContentToDataGridView1(fileContent);
        }
    }


    private Series CreateChartSeries(BindingSource table, string seriesName)
    {
        Series series = new(seriesName)
        {
            ChartType = DrawMode[comboBox1.SelectedIndex]
        };

        foreach (Data dataPoint in table.List)
        {
            series.Points.AddXY(dataPoint.X, dataPoint.Y);
        }

        chart1.Series.Add(series);
        return series;
    }

    private void button4_Click(object sender, EventArgs e)
    {
        BindingSourceData.Clear();

        chart1.Series.Clear();
        _files.Clear();
    }
}
