using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using HotelAndTours.DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QrMenu.ViewComponents.AdminHeader;

    public class AdminHeaderProfile : ViewComponent
    {
        private readonly UserManager<AppUser> _appUser;

        public AdminHeaderProfile(UserManager<AppUser> appUser)
        {
            _appUser = appUser;
        }

        public IViewComponentResult Invoke()
        {
            var context = new Context();

            var Values = context.AppUsers
             .Where(x => x.UserName == User.Identity.Name)
             .Distinct()
             .OrderBy(y => y)
             .ToList();
            //   var user = context.Set<AppUser>().Find(Values);
            return View(Values);
        }
    }
