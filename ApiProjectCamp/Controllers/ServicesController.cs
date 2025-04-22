using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebApi.Context;
using ApiProjectCamp.WebApi.Entities;

namespace ApiProjectCamp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly ApiContext _context;

    public ServicesController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult ServiceList()
    {
        var values = _context.Services.ToList();
        return Ok(values);
    }

    [HttpPost]
    public IActionResult CreateService(Service service)
    {
        _context.Services.Add(service);
        _context.SaveChanges();
        return Ok("Hizmet ekleme işlemi başarılı.");
    }

    [HttpDelete]
    public IActionResult DeleteService(int id)
    {
        var value = _context.Services.Find(id);
        _context.Services.Remove(value);
        _context.SaveChanges();
        return Ok("SHizmet silme işlemi başarılı.");
    }

    [HttpGet("{id}")]
    public IActionResult GetService(int id)
    {
        var value = _context.Services.Find(id);
        return Ok(value);
    }

    [HttpPut]
    public IActionResult UpdateService(Service service)
    {
        _context.Services.Update(service);
        _context.SaveChanges();
        return Ok("Hizmet güncelleme işlemi başarılı.");
    }
}
