﻿namespace ApiProjectCamp.WebUI.Dtos.ContactDtos;

public class ResultContactDto
{
    public int ContactId { get; set; }
    public string MapLocation { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string OpenHours { get; set; }
}
