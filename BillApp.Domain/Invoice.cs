using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain
{
    public class Invoice
    {
        public int Id { get; set; }

        public int InvoiceHeaderId { get; set; }
        public virtual InvoiceHeader InvoiceHeader { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public int Sequence { get; set; }

        public string Prefix { get; set; }

        public string InvoiceNumber { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        public double Total { get; set; }

        public double Tax { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
