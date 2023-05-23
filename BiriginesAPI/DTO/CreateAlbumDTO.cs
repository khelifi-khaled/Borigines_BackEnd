using System.ComponentModel.DataAnnotations;

namespace BiriginesAPI.DTO
{
#nullable disable 

    public class CreateAlbumDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required] 
        public string Title { get;set; }
    }
}
