using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain
{
    public class InvoiceItem
    {
        public int id { get; set; }

        public string item { get; set; }

        public float Quanty { get; set; }

        public float ValueUnit { get; set; }

        public float ValueTotal { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
