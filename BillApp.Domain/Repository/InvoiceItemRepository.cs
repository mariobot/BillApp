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

        public InvoiceItem GetInvoiceItemById(string userid, int id)
        {
            InvoiceItem _invoiceI = context.InvoiceItems.Include(x => x.Invoice).Where(x => x.Invoice.AuthorId == userid && x.id == id).FirstOrDefault();
            return _invoiceI;
        }

        public List<InvoiceItem> GetItemsOfInvoice(string userid, int invoiceid)
        {
            List<InvoiceItem> _ItemsOfInvoice = context.InvoiceItems.Where(x => x.Invoice.AuthorId == userid && x.InvoiceId == invoiceid).ToList();
            return _ItemsOfInvoice;
        }

        public void UpdateInvoiceItem(InvoiceItem _invoiceI)
        {
            context.Entry(_invoiceI).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteInvoiceItem(string userid, int id)
        {
            InvoiceItem _invoiceI = context.InvoiceItems.Include(x => x.Invoice).Where(x => x.Invoice.AuthorId == userid && x.id == id).FirstOrDefault();
            context.InvoiceItems.Remove(_invoiceI);
            context.SaveChanges();
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
