using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BiriginesAPI.DTO
{
#nullable disable 

    public class CreateUserDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [JsonPropertyName("first_name")]
        public string  First_name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [JsonPropertyName("last_name")]
        public string Last_name { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 5)]
        [EmailAddress]
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        [JsonPropertyName("password")]
        public string Password { get; set; }

    }//end class 
}//end name space 
