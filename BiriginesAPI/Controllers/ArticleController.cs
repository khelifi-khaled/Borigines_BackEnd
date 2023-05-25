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
                return NotFound(new { message  = "No Article found in our Data Base"});
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

        //test ok 
        [HttpGet("GetArticleByIdEdit/{id}")]
        public IActionResult GetArticleByIdEdit(int id)
        {
            Article? art = _disptacher.Dispatch(new GetArticleForEditeQuery(id));
            if(art is null)
            {
                return NotFound(new { message = $" Article N° {id} not found in our Data Base" });
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

        //test ok 
        [HttpPost("PostArticle")]
        public IActionResult PostArticle([FromBody] CreateArticleDTO dto)
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

        //no test 
        [HttpPost("PostPicture/{id}")]
        public async Task <IActionResult> PostPicture(int id ,[FromBody] UploadPicturesDOT dto)
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

        //test ok 
        [HttpPut("UpdateArticle/{id}")]
        public IActionResult UpdateArticle(int id, [FromBody] UpdateAtricleDTO Dto)
        {
            CQRS.IResult result = _disptacher.Dispatch(
                new UpdateArticleCommand(
                    id,
                    Dto.CategoryId,
                    Dto.TitelFr,
                    Dto.TitelEn,
                    Dto.TitelNl,
                    Dto.ContentFr,
                    Dto.ContentEn,
                    Dto.ContentNl
                    )
                );

            if(result.IsSuccess)
            {
                return Ok(); 
            }
            return BadRequest(new {message = result.Message});
        }

        //test ok 
        [HttpDelete("DeletePicture/{id}")]
        public IActionResult DeletePicture(int id ,DeletePictureDTO dto)
        {
            CQRS.IResult result = _disptacher.Dispatch(new DeletePictureCommand(id,dto.Name_Picture));

            //If somthing wrong so i will return Bad request 
            if (result.IsFailure)
            {
                return BadRequest(new { message = result.Message });
            }
            try
            {
                //geting pic path 
                string path = Path.Combine(_env.WebRootPath, "Images/", dto.Name_Picture);
                //if my pic don't exists on server 
                if (!System.IO.File.Exists(path))
                {
                    return NotFound(new {message = "Picture not found"});
                }
                //delete pic from server 
                System.IO.File.Delete(path);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        //test ok 
        [HttpDelete("DeleteArticleInfos/{id}")]
        public IActionResult DeleteArticleInfos(int id)
        {
            CQRS.IResult result = _disptacher.Dispatch(new DeleteArticleInfosCommand(id));
           
            if (result.IsFailure)
            {
                return BadRequest( new {message = result.Message });
            }

            return Ok(); 
        }
    }
}
