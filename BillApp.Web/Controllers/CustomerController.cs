using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillApp.Domain;
using BillApp.Domain.Repository;
using Microsoft.AspNet.Identity;

namespace BillApp.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private CustomerRepository _repo;

        public CustomerController(){
            _repo = new CustomerRepository();
        }

        // GET: Customer
        public ActionResult Index()
        {
            var customers = _repo.GetCustomersByUserId(User.Identity.GetUserId());
            return View(customers.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {            
            Customer customer = _repo.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Document,Name,SurName,Phone,Email")] Customer customer)
        {
            customer.AuthorId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                _repo.AddCustomer(customer);
                return RedirectToAction("Index");
            }            
            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {   
            Customer customer = _repo.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }            
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Document,Name,SurName,Phone,Email,AuthorId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateCustomer(customer);                
                return RedirectToAction("Index");
            }            
            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {   
            Customer customer = _repo.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteCustomer(id);            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
