using BiriginesAPI.DTO;
using Borigines.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Models.Queries;
using Tools.CQRS;
using Tools.JWT.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BiriginesAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IDisptacher _disptacher;


        public ArticleController(IDisptacher disptacher)
        {
            _disptacher = disptacher;
        }


        // GET: api/<ArticleController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_disptacher.Dispatch(new GetArticlesQuery()).ToList());
        }


        
        // GET api/<ArticleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Article article = _disptacher.Dispatch(new GetArticleQuery(id));
            if(article is null )
            {
                return NotFound();
            }
            IEnumerable < Picture >? pics = _disptacher.Dispatch(new GetArticlePicturesQuery(id));
            GetArticleDTO DTOtoSend = new(article , pics); 
           
            return Ok(DTOtoSend);
        }

        // POST api/<ArticleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
