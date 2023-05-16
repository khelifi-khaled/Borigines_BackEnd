using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BiriginesAPI.DTO
{
#nullable disable

    public class UserLoginDTO
    {
        [Required]
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
