using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HotelAndTours.ModelsLayer.Models;

using System.Text;
using HotelAndTours.WebUI.Dtos.AppUserDto;

namespace HotelAndTours.WebUI.Controllers
{
    [AllowAnonymous]
    // [Authorize(Roles = "Admin,Sistem")]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel userSign)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    Email = userSign.Mail,
                    UserName = userSign.UserName,
                    NameSurname = userSign.NameSurname,
                    ImageURL = "/Admin/assets/images/users/2.jpg"
                };

                var result = await _userManager.CreateAsync(appUser, userSign.Password);

                if (result.Succeeded)
                {
                    Logger.LogMessage("Sisteme " + userSign.UserName + " kullanıcısı eklendi", User.Identity.Name, LogLevel.Information, HttpContext);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        Logger.LogMessage(userSign.UserName + " kullanıcı sisteme eklenirken hata oluştu:" + item.Description, User.Identity.Name, LogLevel.Error, HttpContext);
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
    }
}