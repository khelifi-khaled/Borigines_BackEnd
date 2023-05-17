using Microsoft.AspNetCore.Mvc;
using Models.Queries;
using Tools.CQRS;

namespace BiriginesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IDisptacher _disptacher;

        public CategoryController(IDisptacher disptacher)
        {
            _disptacher = disptacher;
        }

        [HttpGet("GetCategories")]
        public IActionResult GetCategories()
        {
            return Ok(_disptacher.Dispatch(new GetCategoriesQuery()));
        }

        
       

       
    }
}
