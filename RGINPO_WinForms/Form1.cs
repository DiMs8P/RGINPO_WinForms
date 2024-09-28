using System.ComponentModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace RGINPO_WinForms;

public partial class Form1 : Form
{
    private readonly Dictionary<int, SeriesChartType> DrawMode = new()
    {
        { 0, SeriesChartType.Line},
        { 1, SeriesChartType.Spline},
        {-1, SeriesChartType.Line},
    };
    public BindingSource BindingSourceData { get; set; } = [];
    public Series2D CurrentSeries { get; set; }

    public Form1()
    {
        InitializeComponent();
        InitializeChart();
        InitializeDataGridViews();

        Func(chart1.DrawingArea);
    }

    private void Func(Rectangle rectangle)
    {
        textBox1.Text = $"{rectangle.LeftBottom.X};{rectangle.LeftBottom.Y}";
        textBox2.Text = $"{rectangle.RightTop.X};{rectangle.RightTop.Y}";
    }

    private void InitializeChart()
    {
        CurrentSeries = new("Default")
        {
            Points = [new Data(0, 0), new Data(50, 50)],
            Color = Color.Red,
            BorderWidth = 3
        };

        chart1.Initialize();

        chart1.OnDrawAreaChanged += Func;
        chart1.Series.Add(CurrentSeries);
    }

    private void InitializeDataGridViews()
    {
        dataGridView1.DataSource = BindingSourceData;
        BindingSourceData.ListChanged += BindingSourceData_ListChanged;

        AddPoint(new Data(0, 0));
        AddPoint(new Data(50, 50));

        DataGridViewTextBoxColumn fileNameColumn = new()
        {
            HeaderText = "File name"
        };
        dataGridView2.Columns.Add(fileNameColumn);
        dataGridView2.CellContentClick += DataGridView2_CellContentClick;

        comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        comboBox1.SelectedIndex = 0;
    }

    private void BindingSourceData_ListChanged(object? sender, ListChangedEventArgs e)
    {
        UpdateCurrentSeries();
    }

    private void UpdateCurrentSeries()
    {
        CurrentSeries.Points = BindingSourceData.Cast<Data>().Select(p => new Data(p.X, p.Y)).ToArray();
    }

    private void SelectSeries(Series2D series)
    {
        if (CurrentSeries is not null)
        {
            CurrentSeries.BorderWidth = 1;
        }

        CurrentSeries = series;
        CurrentSeries.BorderWidth = 5;
        UpdateBindingSource(() =>
        {
            foreach (Data point in CurrentSeries.Points)
            {
                BindingSourceData.Add(new Data(point.X, point.Y));
            }
        });
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        AddPoint(new Data(0, 0));
    }

    private void AddPoint(Data data)
    {
        BindingSourceData.Add(data);
    }

    private void ComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
    {
        UpdateCurrentSeries();
        /*foreach (Series item in chart1.Series)
        {
            item.ChartType = DrawMode[comboBox1.SelectedIndex];
        }*/
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

        if (chart1.Series[0].Name == "Default")
        {
            chart1.Series.Clear();
            CurrentSeries = null;
        }

        foreach (string fullFileName in openFileDialog.FileNames)
        {
            string fileName = Path.GetFileName(fullFileName);

            dataGridView2.Rows.Add(fileName);
            var fileContent = ReadFileContent(fullFileName);
            SelectSeries(CreateChartSeries(fileName));
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

    private void UpdateBindingSource(Action function)
    {
        BindingSourceData.ListChanged -= BindingSourceData_ListChanged;
        dataGridView1.Rows.Clear();
        BindingSourceData.Clear();

        function();

        BindingSourceData.ListChanged += BindingSourceData_ListChanged;
    }

    private void AddFileContentToDataGridView1(IReadOnlyList<string> fileContent)
    {
        UpdateBindingSource(() =>
        {
            foreach (var values in fileContent.Select(line => line.Split(' ')))
            {
                var x = double.Parse(values[0]);
                var y = double.Parse(values[1]);

                BindingSourceData.Add(new Data(x, y));
            }

            UpdateCurrentSeries();
        });
    }

    private void DataGridView2_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && e.RowIndex < dataGridView2.Rows.Count)
        {
            string fileName = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

            SelectSeries(chart1.Series.FindByName(fileName!));
        }
    }

    private Series2D CreateChartSeries(string seriesName)
    {
        Series2D series = new(seriesName)
        {
            // ChartType = DrawMode[comboBox1.SelectedIndex]
        };

        chart1.Series.Add(series);

        return series;
    }

    private void button4_Click(object sender, EventArgs e)
    {
        if (BindingSourceData.Count > 0)
        {
            BindingSourceData.RemoveAt(BindingSourceData.Count - 1);
        }
    }

    private void MouseMove_Handler(object sender, MouseEventArgs e)
    {
        textBox3.Text = e.Location.ToString();
    }
}
