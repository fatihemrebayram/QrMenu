using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QrMenu.EntityLayer.Concrete;
using QrMenu.ModelsLayer.Models.Category;
using System.Net.Http;

namespace QrMenu.WebUI.Areas.Frontend.Controllers;
[Area("Frontend")]

    public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
        {
        var client = _httpClientFactory.CreateClient();

        var responseMessage = await client.GetAsync(_configuration["AppSettings:ApiEndpoint"] + "api/Category");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Category>>(jsData);

            var viewModel = new CategoryCrudViewModel
            {
                CategoriesViewModel = values
            };

          
            return View(viewModel);
        }

        return NoContent();
    }
    }

