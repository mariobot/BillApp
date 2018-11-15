using BillApp.Domain.Repository;
using BillApp.Web.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BillApp.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        DashboardRepository _repoDash = new DashboardRepository();

        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            DashboardViewModels dashVM = new DashboardViewModels()
            {
                NumInvoices = await _repoDash.GetNumInvoices(User.Identity.GetUserId()),
                TotalInvoices = await _repoDash.GetTotalInvoices(User.Identity.GetUserId()),
                NumCustomers = await _repoDash.GetTotalCustomers(User.Identity.GetUserId()),
                NumOfArticles = await _repoDash.GetNumOfArticles(User.Identity.GetUserId())
            };
            return View(dashVM);
        }
    }
}