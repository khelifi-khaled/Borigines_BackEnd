using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BiriginesAPI.DTO
{
#nullable disable 

    public class UpdateAtricleDTO
    {
  
        [Required]
        [JsonPropertyName("category_id")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        [JsonPropertyName("titel_fr")]
        public string TitelFr { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [JsonPropertyName("titel_en")]
        public string TitelEn { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        [JsonPropertyName("titel_nl")]
        public string TitelNl { get; set; }

        [Required]
        [JsonPropertyName("content_fr")]
        public string ContentFr { get; set; }

        [Required]
        [JsonPropertyName("content_en")]
        public string ContentEn { get; set; }

        [Required]
        [JsonPropertyName("content_nl")]
        public string ContentNl { get; set; }


    }
}
