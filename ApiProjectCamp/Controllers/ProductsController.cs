using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebApi.Context;
using Microsoft.EntityFrameworkCore;
using ApiProjectCamp.WebApi.Entities;
using ApiProjectCamp.WebApi.Dtos.ProductDtos;

namespace ApiProjectCamp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IValidator<Product> _validator;
    private readonly ApiContext _context;

    public ProductsController(IMapper mapper, IValidator<Product> validator, ApiContext context)
    {
        _mapper = mapper;
        _validator = validator;
        _context = context;
    }

    [HttpGet]
    public IActionResult ProductList()
    {
        var values = _context.Products.ToList();
        return Ok(values);
    }

    [HttpPost]
    public IActionResult CreateProduct(Product product)
    {
        var validationResult = _validator.Validate(product);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
        }
        else
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(new { Message = "Ürün ekleme işlemi başarılı.", data = product });
        }
    }

    [HttpDelete]
    public IActionResult DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        _context.Products.Remove(product);
        _context.SaveChanges();
        return Ok(new { Message = "Ürün silme işlemi başarılı.", data = product });
    }

    [HttpGet("GetProduct")]
    public IActionResult GetProduct(int id)
    {
        var product = _context.Products.Find(id);
        return Ok(product);
    }

    [HttpPut]
    public IActionResult UpdateProduct(Product product)
    {
        var validationResult = _validator.Validate(product);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
        }
        else
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return Ok(new { Message = "Ürün güncelleme işlemi başarılı.", data = product });
        }
    }

    [HttpPost("CreateProductWithCategory")]
    public IActionResult CreateProductWithCategory(CreateProductDto cpdto)
    {
        var value = _mapper.Map<Product>(cpdto);
        _context.Products.Add(value);
        _context.SaveChanges();
        return Ok("Ürün ekleme işlemi başarılı.");
    }

    [HttpGet("ProductListWithCategory")]
    public IActionResult ProductListWithCategory()
    {
        var value = _context.Products.Include(x => x.Category).ToList();
        return Ok(_mapper.Map<List<ResultProductWithCategoryDto>>(value));
    }
}
