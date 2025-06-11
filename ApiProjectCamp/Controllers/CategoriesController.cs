using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebApi.Context;
using ApiProjectCamp.WebApi.Entities;
using ApiProjectCamp.WebApi.Dtos.CategoryDtos;

namespace ApiProjectCamp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApiContext _context;

    public CategoriesController(IMapper mapper, ApiContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult CategoryList()
    {
        var categories = _context.Categories.ToList();
        return Ok(_mapper.Map<List<ResultCategoryDto>>(categories));
    }

    [HttpPost]
    public IActionResult CreateCategory(CreateCategoryDto ccdto)
    {
        var category = _mapper.Map<Category>(ccdto);
        _context.Categories.Add(category);
        _context.SaveChanges();
        return Ok("Ekleme işlemi başarılı.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var category = _context.Categories.Find(id);
        _context.Categories.Remove(category);
        _context.SaveChanges();
        return Ok("Silme işlemi başarılı.");
    }

    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
        var category = _context.Categories.Find(id);
        return Ok(_mapper.Map<GetByIdCategoryDto>(category));
    }

    [HttpPut]
    public IActionResult UpdateCategory(UpdateCategoryDto ucdto)
    {
        var category = _mapper.Map<Category>(ucdto);
        _context.Categories.Update(category);
        _context.SaveChanges();
        return Ok("Güncelleme işlemi başarılı.");
    }
}
