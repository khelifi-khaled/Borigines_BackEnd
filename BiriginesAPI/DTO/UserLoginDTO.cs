using System.ComponentModel.DataAnnotations;

namespace BiriginesAPI.DTO
{
#nullable disable

    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(8)]
        public string Password { get; set; }

    }
}
