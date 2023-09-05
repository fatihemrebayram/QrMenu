using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QrMenu.EntityLayer.Concrete;
using QrMenu.ModelsLayer.Models.Category;
using System.Text;
using QrMenu.BusinessLayer.ValiditonRules.Category;

namespace QrMenu.WebUI.Controllers;

public class CategoryController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index(int editId = 0)
    {
        if (editId != 0)
            ViewBag.EditStatus = "open";

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

            if (editId != 0)
            {
                var responseMessageGetById =
                    await client.GetAsync(_configuration["AppSettings:ApiEndpoint"] + $"api/Category/{editId}");
                if (responseMessageGetById.IsSuccessStatusCode)
                {
                    var jsDataGetById = await responseMessageGetById.Content.ReadAsStringAsync();
                    var valueGetById = JsonConvert.DeserializeObject<Category>(jsDataGetById);
                    viewModel.CategoryViewModel = valueGetById;
                }
            }

            return View(viewModel);
        }

        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> AddEdit(Category p, IFormFile file)
    {
        var validator = new CategoryValidator();
        var validationResult = await validator.ValidateAsync(p);


        if (validationResult.IsValid)
        {
            var client = _httpClientFactory.CreateClient();
            if (file != null)
            {
                var responseMessageGetById =
                    await client.GetAsync(_configuration["AppSettings:ApiEndpoint"] + $"api/Category/{p.Id}");
                if (responseMessageGetById.IsSuccessStatusCode)
                {
                    var jsDataGetById = await responseMessageGetById.Content.ReadAsStringAsync();
                    var valueGetById = JsonConvert.DeserializeObject<Category>(jsDataGetById);
                    var responseMessageFileDelete =
                        await client.DeleteAsync(_configuration["AppSettings:ApiEndpoint"] + $"api/FileImage?path={valueGetById.Image}");
                }
              
                
                var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                var bytes = stream.ToArray();
                var btArrayContent = new ByteArrayContent(bytes);
                btArrayContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                var multipartFormData = new MultipartFormDataContent();
                multipartFormData.Add(btArrayContent, "file", file.FileName);
                var responseMessageFile =
                    await client.PostAsync(_configuration["AppSettings:ApiEndpoint"] + "api/FileImage",
                        multipartFormData);


                if (responseMessageFile.IsSuccessStatusCode)
                {
                    // Get the generated filename from the response
                    var responseContent = await responseMessageFile.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeAnonymousType(responseContent, new { FileName = "" });
                    var generatedFileName = result.FileName;
                    p.Image = _configuration["AppSettings:ApiEndpoint"] + "api/FileImage/images/" +
                                       generatedFileName;

                    // Use the generatedFileName as needed
                }
            }

            var jsdata = JsonConvert.SerializeObject(p);
            var content = new StringContent(jsdata, Encoding.UTF8, "application/json");
            if (p.Id != 0)
            {
                var responseMessage =
                    await client.PutAsync(_configuration["AppSettings:ApiEndpoint"] + "api/Category", content);
                if (responseMessage.IsSuccessStatusCode)
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Category") });
            }
            else
            {
                var responseMessage =
                    await client.PostAsync(_configuration["AppSettings:ApiEndpoint"] + "api/Category", content);
                if (responseMessage.IsSuccessStatusCode)
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Category") });
            }

        }
        foreach (var item in validationResult.Errors)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                Logger.LogMessage(
                    p.Name + " işlem yapılırken hata oluştu: " + item.PropertyName + " " +
                    item.ErrorMessage, User.Identity.Name, LogLevel.Error, HttpContext);
            }

            return Json(new { success = false, errors });
        }


        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync(_configuration["AppSettings:ApiEndpoint"] + $"api/Category/{id}");
        if (responseMessage.IsSuccessStatusCode)
            return RedirectToAction("Index");
        return NotFound();
    }
}