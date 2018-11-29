using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillApp.Domain;

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
            try
            {
                double TotalInvoice = await context.Invoices.Where(x => x.AuthorId == userid && x.InvoiceItems.Count > 0)
                                                       .Include(x => x.InvoiceItems)
                                                       .SumAsync(x => x.InvoiceItems.Sum(z => z.Quanty * z.ValueUnit));
                return TotalInvoice;
            }
            catch (Exception){ return 0; }
        }

        /// <summary>
        /// Get the total of Customers with have invoices
        /// </summary>
        /// <param name="userid">ID User</param>
        /// <returns></returns>
        public async Task<int> GetTotalCustomers(string userid)
        {
            int TotalCustomers = await context.Invoices.Where(x => x.AuthorId == userid)
                                                       .Select(p => p.CustomerId)
                                                       .Distinct()
                                                       .CountAsync();
            return TotalCustomers;
        }

        /// <summary>
        /// Get the number of articles
        /// </summary>
        /// <param name="userid">ID of User</param>
        /// <returns></returns>
        public async Task<double> GetNumOfArticles(string userid)
        {
            try
            {
                double NumOfArticles = await context.Invoices.Where(x => x.AuthorId == userid)
                                                       .Include(x => x.InvoiceItems)
                                                       .SumAsync(x => x.InvoiceItems.Sum(z => z.Quanty));

                return NumOfArticles;
            }
            catch (Exception){ return 0; }
        }

        public List<DateCount> GetDaysWithInvoice(string userid)
        {
            List<DateCount> lista = context.Invoices.Where(x => x.AuthorId == userid)
                                                 .GroupBy(f => new {
                                                     Day = f.DateCreated.Day,                                        
                                                     Month = f.DateCreated.Month,
                                                     Year = f.DateCreated.Year                                                    
                                                 })
                                                 .Select(g => new DateCount
                                                 {
                                                     Day = g.Key.Day,
                                                     Month = g.Key.Month,
                                                     Year = g.Key.Year,
                                                     Count = g.Count()
                                                 }).ToList();
            
            return lista;
        }        

        public void Dispose() {
            context.Dispose();
        }
    }
}
