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

        
        [HttpGet("GetArticleByIdEdit/{id}")]
        public IActionResult GetArticleByIdEdit(int id)
        {
            Article? art = _disptacher.Dispatch(new GetArticleForEditeQuery(id));
            if(art is null)
            {
                return NotFound();
            }
            IEnumerable<Picture>? pics = _disptacher.Dispatch(new GetArticlePicturesQuery(id));
            //mapping 
            GetArticleForEditeDTO DtoToSend = 
                new( art.Id,
                     art.User.Id,
                     art.User.Last_name,
                     art.User.First_name,
                     art.Date,
                     art.CategoryArticle.Id,
                     art.Content.Title,
                     art.Content.Text,
                     art.ContentEn.Title,
                     art.ContentEn.Text,
                     art.ContentNl.Title,
                     art.ContentNl.Text,
                     pics);
            //geting my pics art infos from DB 
           

            return Ok(DtoToSend);
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

        //a modifier
        [HttpPost("PostPicArtilce/{id}")]
        public async Task <IActionResult> PostPicArtilce(int id ,[FromBody] UploadPicturesDOT dto)
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
                    return Ok(new { ImageUrl = result.Message });
                }
                return BadRequest(new { message = result.Message});
            }
            catch (Exception ex )
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //a modifier
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateAtricleDTO Dto)
        {

            return Ok();
        }

        //a modifier
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(id);
        }
    }
}
