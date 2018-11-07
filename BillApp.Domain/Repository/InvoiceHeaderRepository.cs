using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillApp.Domain.Interface;

namespace BillApp.Domain.Repository
{
    public class InvoiceHeaderRepository : IInvoiceHeader
    {
        private BillAppDbContext context = new BillAppDbContext();

        public void AddInvoiceHeader(InvoiceHeader _invoiceH)
        {
            context.InvoiceHeaders.Add(_invoiceH);
            context.SaveChanges();
        }

        public InvoiceHeader GetInvoiceHeaderById(string userid,int id)
        {
            InvoiceHeader _invoiceH = context.InvoiceHeaders.Where(x => x.AuthorId == userid && x.Id == id).FirstOrDefault();
            return _invoiceH;
        }

        public InvoiceHeader GetInvoiceHeaderByUser(string userId)
        {
            InvoiceHeader _invoiceH = context.InvoiceHeaders.Include(x=>x.Author).Where(x => x.AuthorId == userId).FirstOrDefault();
            return _invoiceH;
        }

        public void UpdateInvoiceHeader(InvoiceHeader _invoiceH)
        {
            context.Entry(_invoiceH).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteInvoiceHeader(string userid, int id)
        {
            InvoiceHeader _invoiceH = context.InvoiceHeaders.Where(x => x.AuthorId == userid && x.Id == id).FirstOrDefault();
            context.InvoiceHeaders.Remove(_invoiceH);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
