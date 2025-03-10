﻿using AutoMapper;
using FluentValidation;
using ApiProjectCamp.Context;
using ApiProjectCamp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjectCamp.Controllers
{
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
    }
}
