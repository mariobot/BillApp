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
using BillApp.Web.Models;
using System.Runtime.Remoting.Contexts;

namespace BillApp.Web.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {        

        private InvoiceRepository _repo = new InvoiceRepository();
        private CustomerRepository _repoCustomer = new CustomerRepository();
        private InvoiceHeaderRepository _repoInvHeader = new InvoiceHeaderRepository();
        private InvoiceItemRepository _repoInvItem = new InvoiceItemRepository();

        // GET: Invoice
        public ActionResult Index()
        {
            var invoices = _repo.GetInvoicesByUserId(User.Identity.GetUserId());
            return View(invoices.ToList());
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int id)
        {
            
            Invoice invoice = _repo.GetInvoiceById(User.Identity.GetUserId(),id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            Invoice _invoice = new Invoice();
            InvoiceHeader _invoiceHeader = _repoInvHeader.GetInvoiceHeaderByUser(User.Identity.GetUserId());
            _invoice.InvoiceHeader = _invoiceHeader;
            _invoice.AuthorId = User.Identity.GetUserId();            
            _invoice.DateCreated = DateTime.Now;
            _invoice.InvoiceHeaderId = _invoiceHeader.Id;
            _invoice.Prefix = _invoiceHeader.Prefix;
            _invoice.Sequence = _invoiceHeader.Sequence + 1;
            _invoice.InvoiceNumber = _invoiceHeader.Prefix + _invoiceHeader.Sequence;

            ViewBag.CustomerId = new SelectList(_repoCustomer.GetCustomersByUserId(User.Identity.GetUserId()), "Id", "Document");

            InvoiceViewModels invoiceVM = new InvoiceViewModels();
            invoiceVM.Invoice = _invoice;

            return View(invoiceVM);
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceViewModels invoiceVM)
        {
            if (ModelState.IsValid)
            {                
                invoiceVM.Invoice.CustomerId = Convert.ToInt32(Request.Form["CustomerId"]);
                invoiceVM.Invoice.AuthorId = User.Identity.GetUserId();
                _repo.AddInvoice(invoiceVM.Invoice);
                ViewBag.InvoiceId = invoiceVM.Invoice.Id;
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(_repoCustomer.GetCustomersByUserId(User.Identity.GetUserId()), "Id", "Document", invoiceVM.Invoice.CustomerId);            

            return View(invoiceVM);
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {            
            Invoice invoice = _repo.GetInvoiceById(User.Identity.GetUserId(),id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_repoCustomer.GetCustomersByUserId(User.Identity.GetUserId()), "Id", "Document", invoice.CustomerId);            
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InvoiceHeaderId,CustomerId,AuthorId,Sequence,Prefix,InvoiceNumber,DateCreated,Total,Tax")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateInvoice(invoice);                
                return RedirectToAction("Index");
            }            
            ViewBag.CustomerId = new SelectList(_repoCustomer.GetCustomersByUserId(User.Identity.GetUserId()), "Id", "Document", invoice.CustomerId);            
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {   
            Invoice invoice = _repo.GetInvoiceById(User.Identity.GetUserId(),id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {            
            _repo.DeleteInvoice(User.Identity.GetUserId(),id);         
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddLine(InvoiceViewModels invoiceVM)
        {
            invoiceVM.InvoiceItem.InvoiceId = ViewBag.InvoiceId;
            _repoInvItem.AddInvoiceItem(invoiceVM.InvoiceItem);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
                _repoCustomer.Dispose();
                _repoInvHeader.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
