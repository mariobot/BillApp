using System.Web.Mvc;

namespace BillApp.Web.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ByDates(string startDate, string endDate)
        {

            return View();
        }
        
    }
}