using Borigines.Models.Entities;

namespace BiriginesAPI.DTO
{
#nullable disable 

    public class GetAllAlbumsDTO
    {

        public int AlbumId { get; set; }

        public int UserId { get; set; }

        public string UserLastName { get; set; }

        public string UserFirstName { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public IEnumerable<Picture> Pictures { get; set; }

    }//end class 
}//end name space 
