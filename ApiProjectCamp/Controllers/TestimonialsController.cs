using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebApi.Context;
using ApiProjectCamp.WebApi.Entities;

namespace ApiProjectCamp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestimonialsController : ControllerBase
{
    private readonly ApiContext _context;

    public TestimonialsController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult TestimonialList()
    {
        var values = _context.Testimonials.ToList();
        return Ok(values);
    }

    [HttpPost]
    public IActionResult CreateTestimonial(Testimonial testimonial)
    {
        _context.Testimonials.Add(testimonial);
        _context.SaveChanges();
        return Ok("Testimonial ekleme işlemi başarılı.");
    }

    [HttpDelete]
    public IActionResult DeleteTestimonial(int id)
    {
        var value = _context.Testimonials.Find(id);
        _context.Testimonials.Remove(value);
        _context.SaveChanges();
        return Ok("Testimonial silme işlemi başarılı.");
    }

    [HttpGet("{id}")]
    public IActionResult GetTestimonial(int id)
    {
        var value = _context.Testimonials.Find(id);
        return Ok(value);
    }

    [HttpPut]
    public IActionResult UpdateTestimonial(Testimonial testimonial)
    {
        _context.Testimonials.Update(testimonial);
        _context.SaveChanges();
        return Ok("Testimonial güncelleme işlemi başarılı.");
    }
}
