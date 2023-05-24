using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace BiriginesAPI.DTO
{
#nullable disable 

    public class UpdateAlbumDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string  Title { get; set; }
    }
}
