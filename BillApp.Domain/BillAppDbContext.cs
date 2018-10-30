using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace BillApp.Domain
{
    public class BillAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Invoice> Invoices { get; set; }        

        public IDbSet<InvoiceHeader> InvoiceHeaders { get; set; }

        public IDbSet<InvoiceItem> InvoiceItems { get; set; }

        public IDbSet<Customer> Customers { get; set; }

        public BillAppDbContext() : base("BillAppDbContext", throwIfV1Schema: false)
        {
        }

        public static BillAppDbContext Create()
        {
            return new BillAppDbContext();
        }
    }
}
