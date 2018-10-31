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

        public Customer GetCustomerById(int id)
        {
            Customer _customer = context.Customers.Find(id);
            return _customer;
        }

        public void UpdateCustomer(Customer _customer)
        {
            context.Entry(_customer).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            Customer _customer = context.Customers.Find(id);
            context.Customers.Remove(_customer);
            context.SaveChanges();
        }
    }
}
