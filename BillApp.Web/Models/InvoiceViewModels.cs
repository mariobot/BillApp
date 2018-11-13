using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillApp.Domain;

namespace BillApp.Web.Models
{
    public class InvoiceViewModels
    {
        public Invoice Invoice { get; set; }
        public InvoiceItem InvoiceItem { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set;}
    }
}