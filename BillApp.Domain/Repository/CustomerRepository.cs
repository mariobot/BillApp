using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain.Repository
{
    public class CustomerRepository
    {
        private BillAppDbContext context = new BillAppDbContext();

        public void AddCustomer(Customer _customer)
        {
            context.Customers.Add(_customer);
            context.SaveChanges();
        }

        public Customer GetCustomerById(string userid, int id)
        {
            Customer _customer = context.Customers.Where(x => x.AuthorId == userid && x.Id == id ).FirstOrDefault();
            return _customer;
        }

        public List<Customer> GetCustomersByUserId(string userid)
        {
            List<Customer> _ListCustomers = context.Customers.Include(x => x.Author).Where(x => x.AuthorId == userid).ToList();
            return _ListCustomers;
        }

        public void UpdateCustomer(Customer _customer)
        {
            context.Entry(_customer).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteCustomer(string userid, int id)
        {
            Customer _customer = context.Customers.Where(x => x.AuthorId == userid && x.Id == id).FirstOrDefault();
            context.Customers.Remove(_customer);
            context.SaveChanges();
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
