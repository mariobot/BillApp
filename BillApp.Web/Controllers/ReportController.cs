using System.Collections.Generic;
using System.Web.Mvc;
using BillApp.Web.Models;
using BillApp.Domain.Repository;
using Microsoft.AspNet.Identity;

namespace BillApp.Web.Controllers
{
    public class ReportController : Controller
    {
        private ReportRepository _repo = new ReportRepository();

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ByDates(string startDate, string endDate)
        {

            SeachInvoiceViewModels SearchVM = new SeachInvoiceViewModels()
            {
                Incoices = new List<Domain.Invoice>()
            };

            return View(SearchVM);
        }

        [HttpPost]
        public ActionResult ByDates(SeachInvoiceViewModels Search)
        {
            SeachInvoiceViewModels SearchVM = new SeachInvoiceViewModels()
            {
                Incoices = _repo.GetInvoicesByDate(Search.SearchVM.startDate, Search.SearchVM.endDate, User.Identity.GetUserId())
            };

            return View(SearchVM);
        }
    }
}