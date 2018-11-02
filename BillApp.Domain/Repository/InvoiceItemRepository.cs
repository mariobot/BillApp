using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain.Repository
{
    public class InvoiceItemRepository
    {
        private BillAppDbContext context = new BillAppDbContext();

        public void AddInvoiceItem(InvoiceItem _invoiceI)
        {
            context.InvoiceItems.Add(_invoiceI);
            context.SaveChanges();
        }

        public InvoiceItem GetInvoiceItemById(int id)
        {
            InvoiceItem _invoiceI = context.InvoiceItems.Find(id);
            return _invoiceI;
        }

        public void UpdateInvoiceItem(InvoiceItem _invoiceI)
        {
            context.Entry(_invoiceI).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteInvoiceItem(int id)
        {
            InvoiceItem _invoiceI = context.InvoiceItems.Find(id);
            context.InvoiceItems.Remove(_invoiceI);
            context.SaveChanges();
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
