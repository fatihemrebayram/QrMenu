using System.Net.Http.Headers;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QrMenu.BusinessLayer.ValiditonRules.Category;
using QrMenu.EntityLayer.Concrete;
using QrMenu.ModelsLayer.Models.Meal;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using QrMenu.BusinessLayer.ValiditonRules.Meal;

namespace QrMenu.WebUI.Controllers;

    public class MealController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;


        public MealController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int editId = 0)
        {
            if (editId != 0)
                ViewBag.EditStatus = "open";

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync(_configuration["AppSettings:ApiEndpoint"] + "api/Meal");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<Meal>>(jsData);

                var responseMessageHotels = await client.GetAsync(_configuration["AppSettings:ApiEndpoint"] + "api/Category");
                var jsDataHotels = await responseMessageHotels.Content.ReadAsStringAsync();
                var Categories = JsonConvert.DeserializeObject<List<Category>>(jsDataHotels);
                var _categories = Categories.Select(r => new SelectListItem
                {
                    Text = r.Name, // Access the Title property of the Room object
                    Value = r.Id.ToString() // Access the RoomId property of the Room object
                }).ToList();

                ViewBag.Categories = _categories;

            var viewModel = new MealCrudViewModel
                {
                    MealsViewModel = values
                };

                if (editId != 0)
                {
                    var responseMessageGetById =
                        await client.GetAsync(_configuration["AppSettings:ApiEndpoint"] + $"api/Meal/{editId}");
                    if (responseMessageGetById.IsSuccessStatusCode)
                    {
                        var jsDataGetById = await responseMessageGetById.Content.ReadAsStringAsync();
                        var valueGetById = JsonConvert.DeserializeObject<Meal>(jsDataGetById);
                        viewModel.MealViewModel = valueGetById;
                    }
                }

                return View(viewModel);
            }

            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(Meal p, IFormFile file)
    {
            var validator = new MealValidator();
            var validationResult = await validator.ValidateAsync(p);


            if (validationResult.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

            if (file != null)
            {
                var responseMessageGetById =
                    await client.GetAsync(_configuration["AppSettings:ApiEndpoint"] + $"api/Meal/{p.Id}");
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
                        await client.PutAsync(_configuration["AppSettings:ApiEndpoint"] + "api/Meal", content);
                    if (responseMessage.IsSuccessStatusCode)
                        return Json(new { success = true, redirectUrl = Url.Action("Index", "Meal") });
                }
                else
                {
                    var responseMessage =
                        await client.PostAsync(_configuration["AppSettings:ApiEndpoint"] + "api/Meal", content);
                    if (responseMessage.IsSuccessStatusCode)
                        return Json(new { success = true, redirectUrl = Url.Action("Index", "Meal") });
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
            var responseMessage = await client.DeleteAsync(_configuration["AppSettings:ApiEndpoint"] + $"api/Meal/{id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return NotFound();
        }
    }
