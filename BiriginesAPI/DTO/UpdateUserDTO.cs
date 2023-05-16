using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("first_name")]
        public string First_name { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [JsonPropertyName("last_name")]
        public string Last_name { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(5)]
        [EmailAddress]
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(8)]
        [JsonPropertyName("password")]
        public string Password { get; set; }

    }
}
