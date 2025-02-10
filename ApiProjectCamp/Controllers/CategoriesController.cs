using ApiProjectCamp.Context;
using ApiProjectCamp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjectCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _context;

        public CategoriesController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok("Kategori işlemi başarılı.");
        }
    }
}
