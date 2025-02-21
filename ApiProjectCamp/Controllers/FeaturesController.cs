using AutoMapper;
using ApiProjectCamp.Context;
using ApiProjectCamp.Entities;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.Dtos.FeatureDtos;

namespace ApiProjectCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public FeaturesController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult FeatureList()
        {
            var features = _context.Features.ToList();
            return Ok(_mapper.Map<List<ResultFeatureDto>>(features));
        }

        [HttpGet("{id}")]
        public IActionResult GetList(int id)
        {
            var feature = _context.Features.Find(id);
            return Ok(_mapper.Map<GetByIdFeatureDto>(feature));
        }

        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDto cfdto)
        {
            var feature = _mapper.Map<Feature>(cfdto);
            _context.Features.Add(feature);
            _context.SaveChanges();
            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeature(int id)
        {
            var feature = _context.Features.Find(id);
            _context.Features.Remove(feature);
            _context.SaveChanges();
            return Ok("Silme işlemi başarılı");
        }

        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDto ufdto)
        {
            var feature = _mapper.Map<Feature>(ufdto);
            _context.Features.Update(feature);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi başarılı.");
        }
    }
}
