using API_eCommerceProject.Data;
using API_eCommerceProject.DTO.Requests;
using API_eCommerceProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using API_eCommerceProject.DTO.Responses;
using Microsoft.Extensions.Localization;
using API_eCommerceProject.Model.Category;
using Microsoft.EntityFrameworkCore;

namespace API_eCommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApplicationDBContext context=new ApplicationDBContext();
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(IStringLocalizer<SharedResource> localizer) {
            _localizer = localizer;
        }


        [HttpGet("")]
        public IActionResult Index([FromQuery] string lang = "en") {
            var cats = context.Categories
                       .Include(c => c.CategoryTranslations)
                      .Where(c=>c.Status==Status.Active)
                      .ToList()
                      .Adapt<List<CategoriesResponseDTO>>();

            var result = cats.Select(
                cat => new {
                    Id = cat.Id,
                    Name = cat.CategoryTranslations
            .FirstOrDefault(t => t.Language == lang)?.Name    // from query param
            ?? cat.CategoryTranslations.FirstOrDefault(t => t.Language == "en")?.Name  // fallback
            ?? "Unnamed"  // default if no translations exist
                }
                );
            return Ok(new { message = _localizer["success"].Value, result });
        }

        //admin
        [HttpGet("all")]
        public IActionResult GetAll([FromQuery] string lang="en")
        {
            var cats = context.Categories.Include(c => c.CategoryTranslations)
                      .OrderByDescending(c=> c.createdAt)
                      .ToList()
                      .Adapt<List<CategoriesResponseDTO>>();
            var result = cats.Select(
                cat=> new { 
                    Id=cat.Id,
                    Name=cat.CategoryTranslations.FirstOrDefault(t=>t.Language == lang).Name
                }
                );

            return Ok(new { message = _localizer["success"].Value, result });
        }

        [HttpGet("{id}")]
        public IActionResult Details([FromRoute] int Id) {
            var c = context.Categories.Find(Id);
            if(c is not null)
                return Ok(c.Adapt<CategoriesResponseDTO>());

            return NotFound(new { message= _localizer["Not Found"].Value }); 
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] CategoriesRequestDTO request) 
        {
            var cat = request.Adapt<Category>();

            context.Categories.Add(cat);
            context.SaveChanges();

            return Ok(new { message = _localizer["success"].Value });
        
        }

        [HttpPatch("{id}")]

        public IActionResult Update([FromRoute]int id,[FromBody] CategoriesRequestDTO request) { 
            var categroy= context.Categories.Find(id);
            if (categroy is null) {
                return NotFound(new { message= _localizer["Not Found"].Value });
            }
            
            context.SaveChanges();
            return Ok(new { message = _localizer["Category Name updated successfully"].Value });

        }

        [HttpPatch("{id}/toggle-status")]

        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var categroy = context.Categories.Find(id);
            if (categroy is  null)
            {
                return NotFound(new { message = _localizer["Not Found"] });
            }
            categroy.Status = categroy.Status==Status.Active? Status.Inactive:Status.Active;
            context.SaveChanges();
            return Ok(new { message = 
                _localizer["status updated successfully"].Value});
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete([FromRoute] int Id) {
            var categroy = context.Categories.Find(Id);
            if (categroy is  null)
            {
                return NotFound(new { message = _localizer["Not Found"].Value });
            }
            context.Remove(categroy);
            context.SaveChanges();
            return Ok(new { message= _localizer["Deleted successfully"].Value });
        }
    }
}
