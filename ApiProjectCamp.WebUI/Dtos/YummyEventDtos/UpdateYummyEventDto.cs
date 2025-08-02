using ApiProjectCamp.Shared.Enums;

namespace ApiProjectCamp.WebUI.Dtos.YummyEventDtos;

public class UpdateYummyEventDto
{
    public int YummyEventId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public StatusType Status { get; set; }
    public decimal Price { get; set; }
}
