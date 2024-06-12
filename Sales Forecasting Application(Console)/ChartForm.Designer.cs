using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using Sales_Forecasting_application_Console;

namespace Sales_Forecasting_Application_Console_
{
    public partial class ChartForm : Form
    {
        public ChartForm(List<SalesRecord> salesRecords, decimal totalSales, decimal forecastedTotalSales)
        {
            InitializeComponent();

            // Aggregate chart
            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Seeding Year Sales",
                    Values = new ChartValues<decimal> { totalSales }
                },
                new ColumnSeries
                {
                    Title = "Forecasted Year Sales",
                    Values = new ChartValues<decimal> { forecastedTotalSales }
                }
            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Labels = new[] { "Total Sales" }
            });
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Sales",
                LabelFormatter = value => value.ToString("C")
            });

            // Breakdown by state chart
            var states = new List<string>();
            var seedingSales = new ChartValues<decimal>();
            var forecastedSales = new ChartValues<decimal>();

            foreach (var record in salesRecords)
            {
                states.Add(record.State);
                seedingSales.Add(record.TotalSales);
                forecastedSales.Add(record.IncrementedSales);
            }

            cartesianChart2.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Seeding Year Sales",
                    Values = seedingSales
                },
                new ColumnSeries
                {
                    Title = "Forecasted Year Sales",
                    Values = forecastedSales
                }
            };

            cartesianChart2.AxisX.Add(new Axis
            {
                Labels = states.ToArray()
            });
            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Sales",
                LabelFormatter = value => value.ToString("C")
            });
        }

        private void InitializeComponent()
        {
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart2 = new LiveCharts.WinForms.CartesianChart();
            this.SuspendLayout();
            // cartesianChart1
            this.cartesianChart1.Location = new System.Drawing.Point(12, 12);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(776, 200);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // cartesianChart2
            this.cartesianChart2.Location = new System.Drawing.Point(12, 218);
            this.cartesianChart2.Name = "cartesianChart2";
            this.cartesianChart2.Size = new System.Drawing.Size(776, 220);
            this.cartesianChart2.TabIndex = 1;
            this.cartesianChart2.Text = "cartesianChart2";
            // ChartForm
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cartesianChart2);
            this.Controls.Add(this.cartesianChart1);
            this.Name = "ChartForm";
            this.Text = "Sales Forecasting Charts";
            this.ResumeLayout(false);

        }

        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private LiveCharts.WinForms.CartesianChart cartesianChart2;
    }
}