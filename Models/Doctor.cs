using System.ComponentModel.DataAnnotations;

namespace BharatMedicsV2.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter the Email")]
        public string Email {  get; set; }
    }
}
