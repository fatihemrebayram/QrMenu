using EntityLayer.Concrete;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelAndTours.WebUI.Controllers;

public class DefaultController : Controller
{
    private readonly UserManager<AppUser> _appUser;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public DefaultController(UserManager<AppUser> appUser, IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _appUser = appUser;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult AccessDenied()
    {
        return PartialView();
    }

    public async Task<PartialViewResult> AdminHeader()
    {
        var values = await _appUser.FindByNameAsync(User.Identity.Name);
        return PartialView(values);
    }

    public async Task<PartialViewResult> AdminSidebar()
    {
        return PartialView();
    }
}