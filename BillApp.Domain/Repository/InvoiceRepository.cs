using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain.Repository
{
    public class InvoiceRepository
    {
        private BillAppDbContext context = new BillAppDbContext();

        public void AddInvoice(Invoice _invoice) {
            context.Invoices.Add(_invoice);
            context.SaveChanges();
        }

        public Invoice GetInvoiceById(int id) {
            Invoice _invoice = context.Invoices.Find(id);
            return _invoice;
        }

        public void UpdateInvoice(Invoice _invoice) {
            context.Entry(_invoice).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteInvoice(int id) {
            Invoice _invoice = context.Invoices.Find(id);
            context.Invoices.Remove(_invoice);
            context.SaveChanges();
        }
    }
}
