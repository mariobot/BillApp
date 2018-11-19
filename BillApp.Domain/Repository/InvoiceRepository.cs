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

        public void AddInvoice(Invoice _invoice, List<InvoiceItem> _items) {

            InvoiceHeader invH = context.InvoiceHeaders.Where(x => x.Id == _invoice.InvoiceHeaderId).FirstOrDefault();
            
            _invoice.InvoiceHeader = null;
            _invoice.DateCreated = DateTime.Now;

            context.Invoices.Add(_invoice);

            foreach (var item in _items)
            {
                item.InvoiceId = _invoice.Id;
                context.InvoiceItems.Add(item);
            }

            invH.Sequence += 1;
            context.Entry(invH).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Invoice GetInvoiceById(string userid, int id) {            
            Invoice _invoice = context.Invoices.Where(x => x.AuthorId == userid && x.Id == id)
                                                .Include(x => x.InvoiceHeader)
                                                .Include(x => x.InvoiceItems)
                                                .Include(x => x.Customer)
                                                .FirstOrDefault();
            _invoice.Total = _invoice.InvoiceItems.Sum(x => x.Quanty * x.ValueTotal);
            _invoice.Tax = _invoice.InvoiceItems.Sum(x => x.Quanty * x.ValueTotal) * 0.19;
            return _invoice;
        }

        public List<Invoice> GetInvoicesByUserId(string userId) {
            List<Invoice> _listInvoices = context.Invoices.Where(x => x.AuthorId == userId)
                .Include(x => x.Customer).Include(x => x.InvoiceHeader).Include(x => x.InvoiceItems ).ToList();
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
