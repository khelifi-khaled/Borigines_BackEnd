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

        public AlbumController(IDisptacher disptacher)
        {
            _disptacher = disptacher;
        }

        [HttpGet("GetAllAlbums")]
        public IActionResult GetAllAlbums()
        {
            //geting all albums infos 
            IEnumerable<Album>? albums = _disptacher.Dispatch(new GetAlbumsQuery());
            //if ther's nothing so return not found 
            if (albums is null || !albums.Any())
            {
                return NotFound();
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

        
        [HttpPost("PostAlbum")]
        public IActionResult PostAlbum(CreateAlbumDTO dto)
        {
            CQRS.IResult result = _disptacher.Dispatch(new CreateAlbumCommand(dto.Title, dto.UserId));
            if(result.IsFailure)
            {
                return BadRequest();
            }
            return Ok(new { IdAlbumInserted = result.Message });
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
