using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemVeGuvenlik.Controllers
{
    //  [Authorize(Roles = "Admin,Sistem,EPosta")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}