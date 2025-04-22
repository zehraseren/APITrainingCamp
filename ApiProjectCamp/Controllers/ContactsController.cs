using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebApi.Entities;
using ApiProjectCamp.WebApi.Context;
using ApiProjectCamp.WebApi.Dtos.ContactDtos;

namespace ApiProjectCamp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : Controller
{
    private readonly ApiContext _context;

    public ContactsController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult ContactList()
    {
        var values = _context.Contacts.ToList();
        return Ok(values);
    }

    [HttpPost]
    public IActionResult CreateContact(CreateContactDto ccdto)
    {
        Contact contact = new Contact
        {
            MapLocation = ccdto.MapLocation,
            Address = ccdto.Address,
            PhoneNumber = ccdto.PhoneNumber,
            OpenHours = ccdto.OpenHours
        };
        _context.Contacts.Add(contact);
        _context.SaveChanges();
        return Ok("Ekleme işlemi başarılı.");
    }

    [HttpDelete]
    public IActionResult DeleteContact(int id)
    {
        var contact = _context.Contacts.Find(id);
        _context.Contacts.Remove(contact);
        _context.SaveChanges();
        return Ok("Silme işlemi başarılı.");
    }

    [HttpGet("{id}")]
    public IActionResult GetContact(int id)
    {
        var contact = _context.Contacts.Find(id);
        return Ok(contact);
    }

    [HttpPut]
    public IActionResult UpdateContact(UpdateContactDto ucdto)
    {
        Contact contact = new Contact
        {
            ContactId = ucdto.ContactId,
            MapLocation = ucdto.MapLocation,
            Address = ucdto.Address,
            PhoneNumber = ucdto.PhoneNumber,
            OpenHours = ucdto.OpenHours,
        };
        _context.Contacts.Update(contact);
        _context.SaveChanges();
        return Ok("Güncelleme işlemi başarılı.");
    }
}
