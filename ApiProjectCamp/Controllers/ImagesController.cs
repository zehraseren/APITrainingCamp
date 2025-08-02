using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebApi.Context;
using ApiProjectCamp.WebApi.Entities;
using ApiProjectCamp.WebApi.Dtos.ImageDtos;

namespace ApiProjectCamp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApiContext _context;

    public ImagesController(IMapper mapper, ApiContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult ImageList()
    {
        var images = _context.Images.ToList();
        return Ok(_mapper.Map<List<ResultImageDto>>(images));
    }

    [HttpPost]
    public IActionResult CreateImage(CreateImageDto cidto)
    {
        var image = _mapper.Map<Image>(cidto);
        _context.Images.Add(image);
        _context.SaveChanges();
        return Ok("Ekleme işlemi başarılı.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteImage(int id)
    {
        var image = _context.Images.Find(id);
        _context.Images.Remove(image);
        _context.SaveChanges();
        return Ok("Silme işlemi başarılı.");
    }

    [HttpGet("{id}")]
    public IActionResult GetImage(int id)
    {
        var Image = _context.Images.Find(id);
        return Ok(_mapper.Map<GetByIdImageDto>(Image));
    }

    [HttpPut]
    public IActionResult UpdateImage(UpdateImageDto uidto)
    {
        var image = _mapper.Map<Image>(uidto);
        _context.Images.Update(image);
        _context.SaveChanges();
        return Ok("Güncelleme işlemi başarılı.");
    }
}
