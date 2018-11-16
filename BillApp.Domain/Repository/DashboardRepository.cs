using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain.Repository
{
    public class DashboardRepository
    {
        private BillAppDbContext context = new BillAppDbContext();

        public async Task<int> GetNumInvoices(string userid)
        {
            int numInvoices = await context.Invoices.Where(x => x.AuthorId == userid).CountAsync() ;
            return numInvoices;
        }

        public async Task<double> GetTotalInvoices(string userid)
        {
            double TotalInvoice = await context.Invoices.Where(x => x.AuthorId == userid)
                                                       .Include(x => x.InvoiceItems)
                                                       .SumAsync(x => x.InvoiceItems.Sum(z => z.Quanty * z.ValueUnit));
            return TotalInvoice;
        }

        public async Task<int> GetTotalCustomers(string userid)
        {
            int TotalCustomers = await context.Invoices.Where(x => x.AuthorId == userid)
                                                       .Select(p => p.CustomerId)
                                                       .Distinct()
                                                       .CountAsync();
            return TotalCustomers;
        }

        public async Task<double> GetNumOfArticles(string userid)
        {
            double NumOfArticles = await context.Invoices.Where(x => x.AuthorId == userid)
                                                       .Include(x => x.InvoiceItems)
                                                       .SumAsync(x => x.InvoiceItems.Sum(z => z.Quanty));

            return NumOfArticles;

        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
