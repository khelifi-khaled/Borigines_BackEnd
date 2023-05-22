using BiriginesAPI.DTO;
using Borigines.Models.Entities;

namespace BiriginesAPI.Mappers
{
    internal static class AlbumMap
    {
        internal static GetAllAlbumsDTO ToGetAllAlbumsDTO(this Album album)
        {
            return new GetAllAlbumsDTO()
            {
                AlbumId = album.Id,
                UserId = album.UserAlbum.Id,
                UserLastName = album.UserAlbum.Last_name,
                UserFirstName = album.UserAlbum.First_name,
                Date = album.Date,
                Title = album.Titel,
            };
        }

    }//end class 
}//end name space 
