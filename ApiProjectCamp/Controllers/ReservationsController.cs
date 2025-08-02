using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebApi.Context;
using ApiProjectCamp.WebApi.Entities;
using ApiProjectCamp.WebApi.Dtos.ReservationDtos;

namespace ApiProjectCamp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApiContext _context;

    public ReservationsController(IMapper mapper, ApiContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult ReservationList()
    {
        var reservations = _context.Reservations.ToList();
        return Ok(_mapper.Map<List<ResultReservationDto>>(reservations));
    }

    [HttpPost]
    public IActionResult CreateReservation(CreateReservationDto crdto)
    {
        TimeSpan reservationHour = TimeSpan.Parse(crdto.ReservationHour);

        var reservation = new Reservation
        {
            NameSurname = crdto.NameSurname,
            Email = crdto.Email,
            PhoneNumber = crdto.PhoneNumber,
            ReservationDate = crdto.ReservationDate.Date,
            ReservationHour = reservationHour,
            PersonCount = crdto.PersonCount,
            Message = crdto.Message,
            ReservationStatus = crdto.ReservationStatus
        };

        _context.Reservations.Add(reservation);
        _context.SaveChanges();
        return Ok("Ekleme işlemi başarılı.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReservation(int id)
    {
        var reservation = _context.Reservations.Find(id);
        _context.Reservations.Remove(reservation);
        _context.SaveChanges();
        return Ok("Silme işlemi başarılı.");
    }

    [HttpGet("{id}")]
    public IActionResult GetReservation(int id)
    {
        var reservation = _context.Reservations.Find(id);
        return Ok(_mapper.Map<GetByIdReservationDto>(reservation));
    }

    [HttpPut]
    public IActionResult UpdateReservation(UpdateReservationDto urdto)
    {
        if (!TimeSpan.TryParse(urdto.ReservationHour, out var reservationHour))
            return BadRequest("Geçersiz saat formatı.");

        var reservation = new Reservation
        {
            ReservationId = urdto.ReservationId,
            NameSurname = urdto.NameSurname,
            Email = urdto.Email,
            PhoneNumber = urdto.PhoneNumber,
            ReservationDate = urdto.ReservationDate.Date,
            ReservationHour = reservationHour,
            PersonCount = urdto.PersonCount,
            Message = urdto.Message,
            ReservationStatus = urdto.ReservationStatus
        };

        _context.Reservations.Update(reservation);
        _context.SaveChanges();
        return Ok("Güncelleme işlemi başarılı.");
    }
}
