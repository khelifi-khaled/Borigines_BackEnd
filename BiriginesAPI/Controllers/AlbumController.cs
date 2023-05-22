using BiriginesAPI.DTO;
using BiriginesAPI.Mappers;
using Borigines.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Models.Queries;
using Tools.CQRS;



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

            //injuction of pics in Albums 
            foreach (GetAllAlbumsDTO item in dtos )
            {
                item.Pictures = _disptacher.Dispatch(new GetAlbumPicturesQuery(item.AlbumId)).ToList();
            }

            return Ok(dtos);
        }

        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
