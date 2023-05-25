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
    public class AlbumController : ControllerBase
    {
        private readonly IDisptacher _disptacher;

        private readonly IWebHostEnvironment _env;

        public AlbumController(IDisptacher disptacher, IWebHostEnvironment env)
        {
            _disptacher = disptacher;
            _env = env;
        }
        //test ok 
        [HttpGet("GetAllAlbums")]
        public IActionResult GetAllAlbums()
        {
            //geting all albums infos 
            IEnumerable<Album>? albums = _disptacher.Dispatch(new GetAlbumsQuery());
            //if ther's nothing so return not found 
            if (albums is null || !albums.Any())
            {
                return NotFound(new { message = "No Album found in Data base " });
            }
            //maping 
            IEnumerable<GetAllAlbumsDTO> dtos = albums.Select(a => a.ToGetAllAlbumsDTO()).ToList();

            //Injection of pics in Albums 
            foreach (GetAllAlbumsDTO item in dtos )
            {
                item.Pictures = _disptacher.Dispatch(new GetAlbumPicturesQuery(item.AlbumId)).ToList();
            }

            return Ok(dtos);
        }

        //test ok 
        [HttpPost("PostAlbum")]
        public IActionResult PostAlbum(CreateAlbumDTO dto)
        {
            CQRS.IResult result = _disptacher.Dispatch(new CreateAlbumCommand(dto.Title, dto.UserId));
            if(result.IsFailure)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(new { IdAlbumInserted = result.Message });
        }

        //no test 
        [HttpPost("PostPicture/{id}")]
        public async Task<IActionResult> PostPicture(int id, [FromBody] UploadPicturesDOT dto)
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

                CQRS.IResult result = _disptacher.Dispatch(new CreatePictureAlbumCommand(id, fileName));

                if (result.IsSuccess)
                {
                    return Ok(new { ImageUrl = result.Message });
                }
                return BadRequest(new { message = result.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //test ok 
        [HttpDelete("DeletePicture/{id}")]
        public IActionResult DeletePicture(int id, DeletePictureDTO dto)
        {
            CQRS.IResult result = _disptacher.Dispatch(new DeletePictureAlbumCommand(id, dto.Name_Picture));

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

        [HttpPut("PutAlbum/{id}")]
        public IActionResult PutAlbum(int id, [FromBody] UpdateAlbumDTO dto)
        {
            CQRS.IResult result = _disptacher.Dispatch(new UpdateAlbumCommand(dto.Title , id));
            if (result.IsFailure)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        
        [HttpDelete("DeleteAlbum/{id}")]
        public IActionResult DeleteAlbum(int id)
        {
            CQRS.IResult result = _disptacher.Dispatch(new DeleteAlbumCommand(id));
            if (result.IsFailure)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }
    }
}
