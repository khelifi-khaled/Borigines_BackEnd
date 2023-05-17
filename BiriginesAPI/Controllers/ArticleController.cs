using BiriginesAPI.DTO;
using Borigines.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Models.Commands;
using Models.Queries;
using Tools.CQRS;
using CQRS = Tools.CQRS.Commands;


namespace BiriginesAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IDisptacher _disptacher;
        private readonly IWebHostEnvironment _env;



        public ArticleController(IDisptacher disptacher, IWebHostEnvironment env)
        {
            _disptacher = disptacher;
            _env = env;
        }


        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Article> articles = _disptacher.Dispatch(new GetArticlesQuery()).ToList();

            if(articles is null)
            {
                return NotFound(new { message  = "No Article Found in our Data Base"});
            }

            return Ok(articles);
        }


        
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Article article = _disptacher.Dispatch(new GetArticleQuery(id));
            if(article is null )
            {
                return NotFound(new { message = $" Article N° {id} Found in our Data Base" });
            }
            IEnumerable < Picture >? pics = _disptacher.Dispatch(new GetArticlePicturesQuery(id));
            GetArticleDTO DTOtoSend = new(article , pics); 
           
            return Ok(DTOtoSend);
        }

        
        [HttpPost("PostArtilce")]
        public IActionResult PostArtilce([FromBody] CreateArticleDTO dto)
        {
            CQRS.IResult result =
                _disptacher.Dispatch(new CreateArticleCommand(
                    dto.CategoryId,
                    dto.UserId,
                    dto.TitelFr,
                    dto.TitelEn,
                    dto.TitelNl,
                    dto.ContentFr,
                    dto.ContentEn,
                    dto.ContentNl));

            if (result.IsSuccess)
            {
                int.TryParse(result.Message, out int Id);
                return Ok(new { IdArticleInserted = Id });
            }
            return BadRequest(new { message =  result.Message });
        
        }

        [HttpPost("PostPicsArtilce/{id}")]
        public async Task <IActionResult> PostPicsArtilce(int id ,[FromBody] UploadPicturesDOT dto)
        {
            try
            {
                _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                using Stream stream = new MemoryStream(dto.ArticlePicture);
                string fileName = Guid.NewGuid().ToString() + dto.FileName;
                string path = Path.Combine(_env.WebRootPath, "Images/", fileName);
                using FileStream stream2 = new(path, FileMode.Create);

                //await picture to server then i can insert my infos on my Data base 
                await stream.CopyToAsync(stream2);

                CQRS.IResult result = _disptacher.Dispatch(new CreatePictureCommand(id, fileName));
                if (result.IsSuccess)
                {
                    return Ok(new { message = result.Message });
                }
                return BadRequest(new { message = result.Message});
            }
            catch (Exception ex )
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

       
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
