﻿namespace ApiProjectCamp.WebApi.Dtos.AboutDtos;

public class CreateAboutDto
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public string VideoCoverImageUrl { get; set; }
    public string VideoImageUrl { get; set; }
    public string Description { get; set; }
    public string ReservationNumber { get; set; }
}
