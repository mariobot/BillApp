using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain.Repository
{
    public class InvoiceHeaderRepository
    {
        private BillAppDbContext context = new BillAppDbContext();

        public void AddInvoiceHeader(InvoiceHeader _invoiceH)
        {
            context.InvoiceHeaders.Add(_invoiceH);
            context.SaveChanges();
        }

        public InvoiceHeader GetInvoiceHeaderById(int id)
        {
            InvoiceHeader _invoiceH = context.InvoiceHeaders.Find(id);
            return _invoiceH;
        }

        public void UpdateInvoiceHeader(InvoiceHeader _invoiceH)
        {
            context.Entry(_invoiceH).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteInvoiceHeader(int id)
        {
            InvoiceHeader _invoiceH = context.InvoiceHeaders.Find(id);
            context.InvoiceHeaders.Remove(_invoiceH);
            context.SaveChanges();
        }
    }
}
