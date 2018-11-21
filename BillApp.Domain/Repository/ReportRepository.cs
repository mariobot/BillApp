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

        public List<Invoice> GetInvoicesByDate(string userid, DateTime StartDate, DateTime EndDate)
        {
            List<Invoice> _invoices = context.Invoices.Where(x => x.AuthorId == userid && x.DateCreated >= StartDate && x.DateCreated <= EndDate)                                                
                                                .Include(x => x.InvoiceItems)
                                                .Include(x => x.Customer)
                                                .ToList();            
            
            return _invoices;
        }

    }
}
