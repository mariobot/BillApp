using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillApp.Domain;

namespace BillApp.Domain.Repository
{
    public class ReportRepository
    {
        private BillAppDbContext context = new BillAppDbContext();        

        public List<Invoice> GetInvoicesByDate(DateTime startDate, DateTime endDate, string userid)
        {
            List<Invoice> _invoices = context.Invoices.Where(x => x.AuthorId == userid && x.DateCreated >= startDate && x.DateCreated <= endDate)                                                
                                                .Include(x => x.InvoiceItems)
                                                .Include(x => x.Customer)
                                                .ToList();            
            
            return _invoices;
        }
    }
}
