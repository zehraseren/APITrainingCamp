using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebApi.Context;
using ApiProjectCamp.WebApi.Entities;

namespace ApiProjectCamp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly ApiContext _context;

    public EventsController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult EventList()
    {
        var values = _context.YummyEvents.ToList();
        return Ok(values);
    }

    [HttpPost]
    public IActionResult CreateEvent(YummyEvent yummyEvent)
    {
        _context.YummyEvents.Add(yummyEvent);
        _context.SaveChanges();
        return Ok("Etkinlik ekleme işlemi başarılı.");
    }

    [HttpDelete]
    public IActionResult DeleteEvent(int id)
    {
        var value = _context.YummyEvents.Find(id);
        _context.YummyEvents.Remove(value);
        _context.SaveChanges();
        return Ok("Etkinlik silme işlemi başarılı.");
    }

    [HttpGet("{id}")]
    public IActionResult GetEvent(int id)
    {
        var value = _context.YummyEvents.Find(id);
        return Ok(value);
    }

    [HttpPut]
    public IActionResult UpdateEvent(YummyEvent yummyEvent)
    {
        _context.YummyEvents.Update(yummyEvent);
        _context.SaveChanges();
        return Ok("Etkinlik güncelleme işlemi başarılı.");
    }
}
