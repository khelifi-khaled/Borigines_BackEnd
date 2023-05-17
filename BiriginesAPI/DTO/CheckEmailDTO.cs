using System.ComponentModel.DataAnnotations;

namespace BiriginesAPI.DTO
{
#nullable disable 

    public class CheckEmailDTO
    {
        [Required]
        public string EmailToCheck { get; set; }
    }
}
