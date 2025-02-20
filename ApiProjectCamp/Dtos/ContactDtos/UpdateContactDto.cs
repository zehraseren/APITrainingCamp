namespace ApiProjectCamp.Dtos.ContactDtos
{
    public class UpdateContactDto
    {
        public int ContactId { get; set; }
        public string MapLocation { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string OpenHours { get; set; }
    }
}
