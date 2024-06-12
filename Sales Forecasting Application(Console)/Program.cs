using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using System.Data.SqlClient;
using Sales_Forecasting_Application_Console_;

namespace Sales_Forecasting_application_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=LAPTOP-D571AINA;Database=SuperstoreDB;User Id=username;Password=password;";
            SalesData salesData = new SalesData(connectionString);

            Console.WriteLine("Enter the year to query sales data:");
            int year = int.Parse(Console.ReadLine());

            List<SalesRecord> salesRecords = salesData.GetSalesData(year);

            decimal totalSales = salesRecords.Sum(record => record.TotalSales);
            Console.WriteLine("Total Sales for the year " + year + ": " + totalSales);

            Console.WriteLine("Enter the percentage increment:");
            decimal incrementPercentage = decimal.Parse(Console.ReadLine());

            foreach (var record in salesRecords)
            {
                record.IncrementedSales = record.TotalSales * (1 + (incrementPercentage / 100));
            }

            decimal forecastedTotalSales = salesRecords.Sum(record => record.IncrementedSales);
            Console.WriteLine("Total Forecasted Sales for the year " + (year + 1) + ": " + forecastedTotalSales);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChartForm(salesRecords, totalSales, forecastedTotalSales));
        }
    }

    public class SalesData
    {
        private string connectionString;

        public SalesData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<SalesRecord> GetSalesData(int year)
        {
            var salesData = new List<SalesRecord>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"Query 1";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@year", year);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesData.Add(new SalesRecord
                            {
                                State = reader["State"].ToString(),
                                TotalSales = Convert.ToDecimal(reader["TotalSales"])
                            });
                        }
                    }
                }
            }

            return salesData;
        }
    }

    public class SalesRecord
    {
        public string State { get; set; }
        public decimal TotalSales { get; set; }
        public decimal IncrementedSales { get; set; }
    }
}
