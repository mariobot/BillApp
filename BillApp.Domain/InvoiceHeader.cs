using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain
{
    public class InvoiceHeader
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Identification { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int Sequence { get; set; }

        public string Prefix { get; set; }
        
    }
}
