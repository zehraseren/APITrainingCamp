namespace ApiProjectCamp.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationHour { get; set; }
        public int PersonCount { get; set; }
        public string Message { get; set; }
        public string ReservationStatus { get; set; }
    }
}
