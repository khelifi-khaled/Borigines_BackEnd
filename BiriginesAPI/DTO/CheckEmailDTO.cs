using System.ComponentModel.DataAnnotations;

namespace BiriginesAPI.DTO
{
    public class CheckEmailDTO
    {
        [Required]
        public string EmailToCheck { get; set; }
    }
}
