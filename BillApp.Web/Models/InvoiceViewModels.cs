using BillApp.Domain;
using System.Collections.Generic;

namespace BillApp.Web.Models
{
    public class InvoiceViewModels
    {
        public Invoice Invoice { get; set; }
        public InvoiceItem InvoiceItem { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set;}
    }
}