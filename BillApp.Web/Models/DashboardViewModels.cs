using System;

namespace BillApp.Web.Models
{
    public class DashboardViewModels
    {
        public int NumInvoices { get; set; }
        public double TotalInvoices { get; set; }
        public int NumCustomers { get; set; }
        public double NumOfArticles { get; set; }
    }

    public class MonthViewModels
    {
        public DateTime Date { get; set; }
        public int CountInvoice { get; set; }
        public string Day { get; set; }
    }
}