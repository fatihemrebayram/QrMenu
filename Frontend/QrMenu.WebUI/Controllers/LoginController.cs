using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HotelAndTours.ModelsLayer.Models;
using HotelAndTours.ModelsLayer.Models.Token;
using HotelAndTours.WebUI.Dtos.AppUserDto;
using Microsoft.IdentityModel.Tokens;

namespace HotelAndTours.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            ViewBag.Url = returnUrl;
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel user, [FromForm] string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.password, user.RememberMe, true);
                if (result.Succeeded)
                {
                    // Log the successful login attempt
                    Logger.LogMessage("Sisteme giriş yapıldı.}", user.UserName, LogLevel.Information, HttpContext);

                    // Get the roles for the current user
                    var appUser = await _userManager.FindByNameAsync(user.UserName);
                    var roles = await _userManager.GetRolesAsync(appUser);
                    var tokenViewModel = new CreateTokenViewModel();

                    // Generate the JWT token
                    var token = tokenViewModel.TokenCreate();

                    // Set the token in the response cookies or session
                    Response.Cookies.Append("Authorization", token);



                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    // Log the failed login attempt
                    Logger.LogMessage("Sisteme giriş yapılmaya çalışıldı ancak başarılı olunamadı.", user.UserName, LogLevel.Error, HttpContext);

                    ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                    return RedirectToAction("Index", "Login");
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}