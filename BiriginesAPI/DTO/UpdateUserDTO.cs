using System.ComponentModel.DataAnnotations;

namespace BiriginesAPI.DTO
{
#nullable disable 

    public class UpdateUserDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string First_name { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Last_name { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(5)]
        [EmailAddress]
        public string Login { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(8)]
        public string Password { get; set; }

    }
}
