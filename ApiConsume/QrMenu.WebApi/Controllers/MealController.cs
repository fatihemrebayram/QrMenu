using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QrMenu.BusinessLayer.Abstract;
using QrMenu.EntityLayer.Concrete;

namespace QrMenu.WebApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }


        [HttpPost]
        public IActionResult AddCategory(Meal p)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            p.AddedDate = DateTime.Now;

            _mealService.AddBL(p);
            return Ok("Başarıyla içerik eklendi");
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            var values = _mealService.GetListQueryableBL();
            return Ok(values);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var roomNumber = _mealService.TGetByID(id);
            _mealService.RemoveBL(roomNumber);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var values = _mealService.TGetByID(id);
            return Ok(values);
        }

        [HttpPut]
        public IActionResult UpdateCategory(Meal p)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            p.AddedDate = DateTime.Now;
            _mealService.UpdateBL(p);
            return Ok("Başarıyla içerik güncellendi");
        }
}

