using AutoMapper;
using ApiProjectCamp.Context;
using ApiProjectCamp.Entities;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.Dtos.MessageDtos;

namespace ApiProjectCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public MessagesController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult MessageList()
        {
            var messages = _context.Messages.ToList();
            return Ok(_mapper.Map<List<ResultMessageDto>>(messages));
        }

        [HttpGet("{id}")]
        public IActionResult GetMessage(int id)
        {
            var message = _context.Messages.Find(id);
            return Ok(_mapper.Map<GetByIdMessageDto>(message));
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto cmdto)
        {
            var message = _mapper.Map<Message>(cmdto);
            _context.Messages.Add(message);
            _context.SaveChanges();
            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpDelete]
        public IActionResult DeleteMessage(int id)
        {
            var message = _context.Messages.Find(id);
            _context.Messages.Remove(message);
            _context.SaveChanges();
            return Ok("Silme işlemi başarılı.");
        }

        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto umdto)
        {
            var message = _mapper.Map<Message>(umdto);
            _context.Messages.Update(message);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi başarılı.");
        }
    }
}
