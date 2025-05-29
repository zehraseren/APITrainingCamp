using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebApi.Context;
using ApiProjectCamp.WebApi.Entities;
using ApiProjectCamp.WebApi.Dtos.NotificationDtos;

namespace ApiProjectCamp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly ApiContext _context;

    public NotificationsController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult NotificationList()
    {
        var values = _context.Notifications.ToList();
        return Ok(values);
    }

    [HttpPost]
    public IActionResult CreateNotification(CreateNotificationDto cndto)
    {
        Notification notification = new Notification
        {
            Description = cndto.Description,
            IconUrl = cndto.IconUrl,
            NotificationDate = cndto.NotificationDate,
            IsRead = cndto.IsRead
        };
        _context.Notifications.Add(notification);
        _context.SaveChanges();
        return Ok("Ekleme işlemi başarılı.");
    }

    [HttpDelete]
    public IActionResult DeleteNotification(int id)
    {
        var notification = _context.Notifications.Find(id);
        _context.Notifications.Remove(notification);
        _context.SaveChanges();
        return Ok("Silme işlemi başarılı.");
    }

    [HttpGet("{id}")]
    public IActionResult GetNotification(int id)
    {
        var notification = _context.Notifications.Find(id);
        return Ok(notification);
    }

    [HttpPut]
    public IActionResult UpdateNotification(UpdateNotificationDto undto)
    {
        Notification notification = new Notification
        {
            NotificationId = undto.NotificationId,
            Description = undto.Description,
            IconUrl = undto.IconUrl,
            NotificationDate = undto.NotificationDate,
            IsRead = undto.IsRead
        };
        _context.Notifications.Update(notification);
        _context.SaveChanges();
        return Ok("Güncelleme işlemi başarılı.");
    }
}
