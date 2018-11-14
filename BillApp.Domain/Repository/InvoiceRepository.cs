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
            InvoiceHeader invH = _invoice.InvoiceHeader;
            _invoice.InvoiceHeader = null;

            context.Invoices.Add(_invoice);

            invH.Sequence += 1;
            context.Entry(invH).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Invoice GetInvoiceById(string userid, int id) {
            Invoice _invoice = context.Invoices.Where(x => x.AuthorId == userid && x.Id == id).FirstOrDefault();
            return _invoice;
        }

        public List<Invoice> GetInvoicesByUserId(string userId) {
            List<Invoice> _listInvoices = context.Invoices.Where(x => x.AuthorId == userId)
                .Include(x => x.Customer).Include(x => x.InvoiceHeader).ToList();
            return _listInvoices;
        }

        public void UpdateInvoice(Invoice _invoice) {
            context.Entry(_invoice).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteInvoice(string userid, int id) {
            Invoice _invoice = context.Invoices.Where(x => x.AuthorId == userid && x.Id == id).FirstOrDefault();
            context.Invoices.Remove(_invoice);
            context.SaveChanges();
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
