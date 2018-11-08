using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BillApp.Domain
{
    public class BillAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public BillAppDbContext() : base("BillAppDbContext", throwIfV1Schema: false){
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;            
        }

        public static BillAppDbContext Create()
        {
            return new BillAppDbContext();
        }

        public IDbSet<Invoice> Invoices { get; set; }

        public IDbSet<InvoiceHeader> InvoiceHeaders { get; set; }

        public IDbSet<InvoiceItem> InvoiceItems { get; set; }

        public IDbSet<Customer> Customers { get; set; }

        //  I cant recomend use this method because generate conflict with Identity init context
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{         
        //    //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //    //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        //}
    }
}
