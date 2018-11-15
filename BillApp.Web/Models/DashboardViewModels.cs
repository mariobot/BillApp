using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillApp.Web.Models
{
    public class DashboardViewModels
    {
        public int NumInvoices { get; set; }
        public float TotalInvoices { get; set; }
        public int NumCustomers { get; set; }
        public float NumOfArticles { get; set; }
    }
}