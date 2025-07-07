using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebApi.Context;
using ApiProjectCamp.WebApi.Entities;
using ApiProjectCamp.WebApi.Dtos.AboutDtos;

namespace ApiProjectCamp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApiContext _context;

    public AboutsController(IMapper mapper, ApiContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    [HttpGet]
    public IActionResult AboutList()
    {
        var Abouts = _context.Abouts.ToList();
        return Ok(_mapper.Map<List<ResultAboutDto>>(Abouts));
    }

    [HttpPost]
    public IActionResult CreateAbout(CreateAboutDto cadto)
    {
        var about = _mapper.Map<About>(cadto);
        _context.Abouts.Add(about);
        _context.SaveChanges();
        return Ok("Ekleme işlemi başarılı.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAbout(int id)
    {
        var about = _context.Abouts.Find(id);
        _context.Abouts.Remove(about);
        _context.SaveChanges();
        return Ok("Silme işlemi başarılı.");
    }

    [HttpGet("{id}")]
    public IActionResult GetAbout(int id)
    {
        var about = _context.Abouts.Find(id);
        return Ok(_mapper.Map<GetByIdAboutDto>(about));
    }

    [HttpPut]
    public IActionResult UpdateAbout(UpdateAboutDto uadto)
    {
        var about = _mapper.Map<About>(uadto);
        _context.Abouts.Update(about);
        _context.SaveChanges();
        return Ok("Güncelleme işlemi başarılı.");
    }
}
