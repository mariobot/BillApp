using BillApp.Domain;
using BillApp.Domain.Repository;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace BillApp.Web.Controllers
{
    [Authorize]
    public class InvoiceHeaderController : Controller
    {
        private InvoiceHeaderRepository _repo;

        public InvoiceHeaderController()
        {
            _repo = new InvoiceHeaderRepository() ;
        }

        // GET: InvoiceHeader
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
            InvoiceHeader invoiceHeader = _repo.GetInvoiceHeaderByUser(userid);
            return View(invoiceHeader);
        }

        // GET: InvoiceHeader/Details/5
        public ActionResult Details(int id)
        {            
            InvoiceHeader invoiceHeader = _repo.GetInvoiceHeaderById(User.Identity.GetUserId(),id);
            if (invoiceHeader == null)
            {
                return HttpNotFound();
            }
            return View(invoiceHeader);
        }

        // GET: InvoiceHeader/Create
        public ActionResult Create()
        {
            // For user you only have one InvoiceHeader
            InvoiceHeader _invH = _repo.GetInvoiceHeaderByUser(User.Identity.GetUserId());

            if (_invH == null)
                return View();
            else
                return RedirectToAction("Edit", "InvoiceHeader", new { id = _invH.Id });
        }

        // POST: InvoiceHeader/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Identification,Adress,Phone,Email,Sequence,Prefix")] InvoiceHeader invoiceHeader)
        {
            invoiceHeader.AuthorId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                _repo.AddInvoiceHeader(invoiceHeader);                
                return RedirectToAction("Index");
            }
            return View(invoiceHeader);
        }

        // GET: InvoiceHeader/Edit/5
        public ActionResult Edit(int id)
        {            
            InvoiceHeader invoiceHeader = _repo.GetInvoiceHeaderById(User.Identity.GetUserId(),id);
            if (invoiceHeader == null)
            {
                return HttpNotFound();
            }            
            return View(invoiceHeader);
        }

        // POST: InvoiceHeader/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Identification,Adress,Phone,Email,Sequence,Prefix,AuthorId")] InvoiceHeader invoiceHeader)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateInvoiceHeader(invoiceHeader);                
                return RedirectToAction("Index");
            }            
            return View(invoiceHeader);
        }

        // GET: InvoiceHeader/Delete/5
        public ActionResult Delete(int id)
        {            
            InvoiceHeader invoiceHeader = _repo.GetInvoiceHeaderById(User.Identity.GetUserId(),id);
            if (invoiceHeader == null)
            {
                return HttpNotFound();
            }
            return View(invoiceHeader);
        }

        // POST: InvoiceHeader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //_repo.DeleteInvoiceHeader(User.Identity.GetUserId(),id);            
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
