namespace ECOLOGViewerver2
{
    partial class Radarchart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.LostEnergylabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.RegeneLosslabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ConvertLosslabel = new System.Windows.Forms.Label();
            this.ClimbingEnergylabel = new System.Windows.Forms.Label();
            this.RollingEnergylabel = new System.Windows.Forms.Label();
            this.AirEnergylabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.LostEnergylabel);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.RegeneLosslabel);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.ConvertLosslabel);
            this.panel1.Controls.Add(this.ClimbingEnergylabel);
            this.panel1.Controls.Add(this.RollingEnergylabel);
            this.panel1.Controls.Add(this.AirEnergylabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(279, 140);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 14;
            // 
            // LostEnergylabel
            // 
            this.LostEnergylabel.AutoSize = true;
            this.LostEnergylabel.Location = new System.Drawing.Point(148, 120);
            this.LostEnergylabel.Name = "LostEnergylabel";
            this.LostEnergylabel.Size = new System.Drawing.Size(0, 12);
            this.LostEnergylabel.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 120);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "Whole Lost Energy[kWh]：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(42, 104);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "Regene Loss[kWh]：";
            // 
            // RegeneLosslabel
            // 
            this.RegeneLosslabel.AutoSize = true;
            this.RegeneLosslabel.Location = new System.Drawing.Point(148, 104);
            this.RegeneLosslabel.Name = "RegeneLosslabel";
            this.RegeneLosslabel.Size = new System.Drawing.Size(0, 12);
            this.RegeneLosslabel.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "Air Resistance[kWh]：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "Convert Loss[kWh]：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "Climbing Resistance[kWh]：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "Rolling Resistance[kWh]：";
            // 
            // ConvertLosslabel
            // 
            this.ConvertLosslabel.AutoSize = true;
            this.ConvertLosslabel.Location = new System.Drawing.Point(148, 86);
            this.ConvertLosslabel.Name = "ConvertLosslabel";
            this.ConvertLosslabel.Size = new System.Drawing.Size(0, 12);
            this.ConvertLosslabel.TabIndex = 4;
            // 
            // ClimbingEnergylabel
            // 
            this.ClimbingEnergylabel.AutoSize = true;
            this.ClimbingEnergylabel.Location = new System.Drawing.Point(148, 69);
            this.ClimbingEnergylabel.Name = "ClimbingEnergylabel";
            this.ClimbingEnergylabel.Size = new System.Drawing.Size(0, 12);
            this.ClimbingEnergylabel.TabIndex = 3;
            // 
            // RollingEnergylabel
            // 
            this.RollingEnergylabel.AutoSize = true;
            this.RollingEnergylabel.Location = new System.Drawing.Point(148, 52);
            this.RollingEnergylabel.Name = "RollingEnergylabel";
            this.RollingEnergylabel.Size = new System.Drawing.Size(0, 12);
            this.RollingEnergylabel.TabIndex = 2;
            // 
            // AirEnergylabel
            // 
            this.AirEnergylabel.AutoSize = true;
            this.AirEnergylabel.Location = new System.Drawing.Point(148, 35);
            this.AirEnergylabel.Name = "AirEnergylabel";
            this.AirEnergylabel.Size = new System.Drawing.Size(0, 12);
            this.AirEnergylabel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "This Trip Data";
            // 
            // Chart
            // 
            this.Chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea1.AxisX.MajorTickMark.Interval = 0D;
            chartArea1.AxisX.MajorTickMark.IntervalOffset = 0D;
            chartArea1.AxisX.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisY.MajorGrid.Interval = 0D;
            chartArea1.AxisY.MajorGrid.IntervalOffset = 0D;
            chartArea1.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisY.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.ScaleBreakStyle.Enabled = true;
            chartArea1.AxisY.ScaleBreakStyle.MaxNumberOfBreaks = 5;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.Chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Chart.Legends.Add(legend1);
            this.Chart.Location = new System.Drawing.Point(-1, 1);
            this.Chart.Name = "Chart";
            series1.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.Percent50;
            series1.BorderColor = System.Drawing.Color.Blue;
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Radar;
            series1.Color = System.Drawing.Color.Blue;
            series1.CustomProperties = "AreaDrawingStyle=Polygon";
            series1.Legend = "Legend1";
            series1.Name = "Average Data";
            series1.XValueMember = "SUBJECT";
            series1.YValueMembers = "AVERAGE";
            series1.YValuesPerPoint = 4;
            series2.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.Percent50;
            series2.BorderColor = System.Drawing.Color.Red;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Radar;
            series2.Color = System.Drawing.Color.Red;
            series2.CustomProperties = "AreaDrawingStyle=Polygon";
            series2.Legend = "Legend1";
            series2.Name = "This Trip Data";
            series2.XValueMember = "SUBJECT";
            series2.YValueMembers = "SCORE";
            this.Chart.Series.Add(series1);
            this.Chart.Series.Add(series2);
            this.Chart.Size = new System.Drawing.Size(845, 590);
            this.Chart.TabIndex = 2;
            this.Chart.Text = "chart1";
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.Name = "Title";
            this.Chart.Titles.Add(title1);
            // 
            // Radarchart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 592);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Chart);
            this.Name = "Radarchart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radarchart";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label RegeneLosslabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label ConvertLosslabel;
        private System.Windows.Forms.Label ClimbingEnergylabel;
        private System.Windows.Forms.Label RollingEnergylabel;
        private System.Windows.Forms.Label AirEnergylabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label LostEnergylabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart;
    }
}