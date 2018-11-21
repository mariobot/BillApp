using BillApp.Domain;
using BillApp.Domain.Repository;
using BillApp.Web.Models;
using Microsoft.AspNet.Identity;
using Rotativa;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

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
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {
            var invoices = _repo.GetInvoicesByUserId(User.Identity.GetUserId());
            return View(invoices);
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int id)
        {            
            Invoice invoice = _repo.GetInvoiceById(User.Identity.GetUserId(), id);
            if (invoice == null)            
                return HttpNotFound();            
            return View(invoice);
        }

        public ActionResult InvoiceToPdf(int id) {

            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }

            return new ActionAsPdf("Details", new { id = id }) { FileName = "Test.pdf" , Cookies = cookieCollection };
        }

        public ActionResult InvoiceListToPdf()
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }

            return new ActionAsPdf("Index") { FileName = "TestIndex.pdf", Cookies = cookieCollection };
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            ValidateInitConfig();

            Invoice _invoice = new Invoice();
            InvoiceHeader _invoiceHeader = _repoInvHeader.GetInvoiceHeaderByUser(User.Identity.GetUserId());
            _invoice.InvoiceHeader = _invoiceHeader;
            _invoice.AuthorId = User.Identity.GetUserId();
            _invoice.InvoiceHeaderId = _invoiceHeader.Id;
            _invoice.Prefix = _invoiceHeader.Prefix;
            _invoice.Sequence = _invoiceHeader.Sequence + 1;
            _invoice.InvoiceNumber = _invoice.Prefix + _invoice.Sequence;

            ViewBag.CustomerId = new SelectList(_repoCustomer.GetCustomersByUserId(User.Identity.GetUserId()), "Id", "Document");
            
            Session["items"] = new List<InvoiceItem>();            

            InvoiceViewModels invoiceVM = new InvoiceViewModels();
            invoiceVM.InvoiceItems = (List<InvoiceItem>)Session["items"];
            invoiceVM.Invoice = _invoice;

            return View(invoiceVM);
        }

        [HttpPost]
        public ActionResult Create(InvoiceViewModels invoiceVM) {

            if (((List<InvoiceItem>)Session["items"]).Any())
            {
                List<InvoiceItem> _items = (List<InvoiceItem>)Session["items"];
                _repo.AddInvoice(invoiceVM.Invoice, _items);
                return RedirectToAction("Index", "Invoice");
            }
            else
            {
                TempData["errorItems"] = "Es necesario adicionar lineas a la factura.";
            }

            ViewBag.CustomerId = new SelectList(_repoCustomer.GetCustomersByUserId(User.Identity.GetUserId()), "Id", "Document",invoiceVM.Invoice.CustomerId);

            invoiceVM.InvoiceItems = (List<InvoiceItem>)Session["items"];
            return View(invoiceVM);
        }

        [HttpPost]
        public ActionResult AddLineSession(InvoiceViewModels invoiceVM)
        {
            List<InvoiceItem> _items = (List<InvoiceItem>)Session["items"];
            invoiceVM.InvoiceItem.ValueTotal = invoiceVM.InvoiceItem.Quanty * invoiceVM.InvoiceItem.ValueUnit;
            _items.Add(invoiceVM.InvoiceItem);
            Session["items"] = _items;
            return PartialView("_InvItemsSession", _items);
        }

        [HttpPost]
        public ActionResult DeleteLineSession(int id)
        {
            List<InvoiceItem> _items = (List<InvoiceItem>)Session["items"];
            _items.RemoveAt(id);
            Session["items"] = _items;
            return PartialView("_InvItemsSession", _items);
        }

        [HttpPost]
        public ActionResult LoadEditLineSession(int id)
        {
            List<InvoiceItem> _items = (List<InvoiceItem>)Session["items"];
            InvoiceItem _itemToEdit = _items.ElementAt(id);
            Session["EditItem"]= id;
            return PartialView("_EditLineSession", _itemToEdit);
        }

        [HttpPost]
        public ActionResult EditLineSession(InvoiceItem _invoiceItem)
        {
            List<InvoiceItem> _items = (List<InvoiceItem>)Session["items"];
            int editItem = (int)Session["EditItem"];
            _items.ElementAt(editItem).item = _invoiceItem.item;
            _items.ElementAt(editItem).Quanty = _invoiceItem.Quanty;
            _items.ElementAt(editItem).ValueUnit = _invoiceItem.ValueUnit;
            _items.ElementAt(editItem).ValueTotal = _invoiceItem.Quanty * _invoiceItem.ValueUnit;
            Session["items"] = _items;
            return PartialView("_InvItemsSession", _items);
        }

        private ActionResult ValidateInitConfig()
        {
            InvoiceHeader _invoiceH = _repoInvHeader.GetInvoiceHeaderByUser(User.Identity.GetUserId());

            if (_invoiceH == null)
                return RedirectToAction("Create", "InvoiceHeader");

            Customer _customer = _repoCustomer.GetCustomersByUserId(User.Identity.GetUserId()).FirstOrDefault();

            if (_customer == null)
                return RedirectToAction("Create", "Customer");

            return null;
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            Invoice invoice = _repo.GetInvoiceById(User.Identity.GetUserId(), id);
            InvoiceViewModels invoiceVM = new InvoiceViewModels() { Invoice = invoice };
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_repoCustomer.GetCustomersByUserId(User.Identity.GetUserId()), "Id", "Document", invoice.CustomerId);
            return View(invoiceVM);
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
            Invoice invoice = _repo.GetInvoiceById(User.Identity.GetUserId(), id);
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
            _repo.DeleteInvoice(User.Identity.GetUserId(), id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddLine(InvoiceViewModels invoiceVM)
        {
            invoiceVM.InvoiceItem.InvoiceId = invoiceVM.Invoice.Id;
            invoiceVM.InvoiceItem.ValueTotal = invoiceVM.InvoiceItem.Quanty * invoiceVM.InvoiceItem.ValueUnit;
            _repoInvItem.AddInvoiceItem(invoiceVM.InvoiceItem);
            invoiceVM.InvoiceItems = _repoInvItem.GetItemsOfInvoice(User.Identity.GetUserId(), invoiceVM.Invoice.Id);
            return PartialView("_InvItems", invoiceVM.InvoiceItems);
        }

        [HttpPost]
        public ActionResult DeleteLine(int id, int invoiceid)
        {
            _repoInvItem.DeleteInvoiceItem(User.Identity.GetUserId(), id);
            List<InvoiceItem> _items = _repoInvItem.GetItemsOfInvoice(User.Identity.GetUserId(), invoiceid);
            return PartialView("_InvItems", _items);
        }

        [HttpPost]
        public ActionResult LoadEditLine(int id)
        {
            InvoiceItem _invoiceI = _repoInvItem.GetInvoiceItemById(User.Identity.GetUserId(), id);
            return PartialView("_EditLine", _invoiceI);
        }

        [HttpPost]
        public ActionResult EditLine(InvoiceItem _invoiceItem)
        {
            _invoiceItem.ValueTotal = _invoiceItem.Quanty * _invoiceItem.ValueUnit;
            _repoInvItem.UpdateInvoiceItem(_invoiceItem);                        
            List<InvoiceItem> _items = _repoInvItem.GetItemsOfInvoice(User.Identity.GetUserId(), _invoiceItem.InvoiceId);
            return PartialView("_InvItems", _items);
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
