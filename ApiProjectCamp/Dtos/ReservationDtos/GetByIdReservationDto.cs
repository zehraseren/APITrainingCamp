using ApiProjectCamp.Shared.Enums;

namespace ApiProjectCamp.WebApi.Dtos.ReservationDtos;

public class GetByIdReservationDto
{
    public int ReservationId { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime ReservationDate { get; set; }
    public TimeSpan ReservationHour { get; set; }
    public int PersonCount { get; set; }
    public string Message { get; set; }
    public StatusType ReservationStatus { get; set; }
}
