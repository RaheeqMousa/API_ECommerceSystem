using API_eCommerceProject.Data;
using API_eCommerceProject.DTO.Requests;
using API_eCommerceProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using API_eCommerceProject.DTO.Responses;
using Microsoft.Extensions.Localization;
using API_eCommerceProject.Model.Category;

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
        public IActionResult Index() {
            var cats = context.Categories
                      .Where(c=>c.Status==Status.Active)
                      .ToList()
                      .Adapt<List<CategoriesResponseDTO>>();

            return Ok(new { message = _localizer["success"].Value, cats });
        }

        //admin
        [HttpGet("all")]
        public IActionResult GetAllt()
        {
            var cats = context.Categories
                      .OrderByDescending(c=> c.createdAt)
                      .ToList()
                      .Adapt<List<CategoriesResponseDTO>>();

            return Ok(new { message = _localizer["success"].Value, cats });
        }

        [HttpGet("{id}")]
        public IActionResult Details([FromRoute] int Id) {
            var c = context.Categories.Find(Id);
            if(c is not null)
                return Ok(c.Adapt<CategoriesResponseDTO>());

            return NotFound(new { message= _localizer["Not Found"].Value }); 
        }


        [HttpPost("")]
        public IActionResult Create([FromBody] CategoriesRequestDTO request) 
        {
            var cat = request.Adapt<Category>();

            context.Add(cat);
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
