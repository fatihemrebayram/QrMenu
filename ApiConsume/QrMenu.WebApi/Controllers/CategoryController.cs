using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QrMenu.BusinessLayer.Abstract;
using QrMenu.EntityLayer.Concrete;

namespace QrMenu.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public IActionResult AddCategory(Category p)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        p.AddedDate = DateTime.Now;
      
        _categoryService.AddBL(p);
        return Ok("Başarıyla kategori eklendi");
    }

    [HttpGet]
    public IActionResult CategoryList()
    {
        var values = _categoryService.GetListQueryableBL();
        return Ok(values);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var roomNumber = _categoryService.TGetByID(id);
        _categoryService.RemoveBL(roomNumber);
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id)
    {
        var values = _categoryService.TGetByID(id);
        return Ok(values);
    }

    [HttpPut]
    public IActionResult UpdateCategory(Category p)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        p.AddedDate = DateTime.Now;
        _categoryService.UpdateBL(p);
        return Ok("Başarıyla kategori güncellendi");
    }
}