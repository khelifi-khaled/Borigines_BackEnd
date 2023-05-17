using BiriginesAPI.DTO;
using BiriginesAPI.Mappers;
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


        //test ok 
        [HttpGet("GetAllByCategory/{CategoryId}/{language}")]
        public IActionResult GetAllByCategory(int CategoryId , string language)
        {
            IEnumerable<Article> articles = _disptacher.Dispatch(
                new GetArticlesByCategoryQuery(CategoryId,language));

            if(!articles.Any())
            {
                return NotFound(new { message  = "No Article Found in our Data Base"});
            }
            IEnumerable<GetArticlesByCategoryDTO> Dtos = 
                articles.Select(a => a.ToDtoGetArticlesByCategory());

            return Ok(Dtos.ToList());
        }


        
        //test ok 
        [HttpGet("GetArticleById/{id}/{language}")]
        public IActionResult GetArticleById(int id , string language)
        {
            //geting my art from DB 
            Article? article = _disptacher.Dispatch(new GetArticleQuery(id,language));

            if(article is null )
            {
                return NotFound(new { message = $" Article N° {id} not found in our Data Base" });
            }
            //geting my pics art infos from DB 
            IEnumerable < Picture >? pics = _disptacher.Dispatch(new GetArticlePicturesQuery(id));
            //maping my infos from db to my dto to send 
            GetArticleByIdDTO? DTOtoSend =
                new(article.Id,
                article.User.Id,
                article.User.Last_name,
                article.User.First_name,
                article.Date,
                article.CategoryArticle.Id,
                article.Content.Title,
                article.Content.Text,
                pics);
           
            return Ok(DTOtoSend);
        }

        //a modifier
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
                _ = int.TryParse(result.Message, out int Id);
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
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok(new { id , value });
        }

       
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(id);
        }
    }
}
