using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace SistemVeGuvenlik.ViewComponents.Search
{
    public class Search : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}