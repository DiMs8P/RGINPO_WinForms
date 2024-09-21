namespace RGINPO_WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            dataGridView1 = new DataGridView();
            comboBox1 = new ComboBox();
            button2 = new Button();
            button1 = new Button();
            button3 = new Button();
            dataGridView2 = new DataGridView();
            button4 = new Button();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(814, 12);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(280, 173);
            dataGridView1.TabIndex = 0;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "DrawLine", "DrawSpline" });
            comboBox1.Location = new Point(814, 399);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 6;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.Location = new Point(1019, 551);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 8;
            button2.Text = "Load";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Load_Click;
            // 
            // button1
            // 
            button1.Location = new Point(1019, 580);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Save_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1006, 399);
            button3.Margin = new Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new Size(88, 27);
            button3.TabIndex = 1;
            button3.Text = "Add";
            button3.UseVisualStyleBackColor = true;
            button3.Click += AddButton_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(814, 191);
            dataGridView2.Margin = new Padding(4, 3, 4, 3);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(280, 173);
            dataGridView2.TabIndex = 10;
            // 
            // button4
            // 
            button4.Location = new Point(1006, 432);
            button4.Margin = new Padding(4, 3, 4, 3);
            button4.Name = "button4";
            button4.Size = new Size(88, 27);
            button4.TabIndex = 11;
            button4.Text = "Clear";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // chart1
            // 
            chart1.BackColor = Color.MistyRose;
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(12, 12);
            chart1.Name = "chart1";
            chart1.Size = new Size(795, 591);
            chart1.TabIndex = 12;
            chart1.Text = "chart1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1107, 614);
            Controls.Add(chart1);
            Controls.Add(button4);
            Controls.Add(dataGridView2);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(comboBox1);
            Controls.Add(dataGridView1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private ComboBox comboBox1;
        private Button button2;
        private Button button1;
        private Button button3;
        private DataGridView dataGridView2;
        private Button button4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
