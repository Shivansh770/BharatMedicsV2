using System.ComponentModel.DataAnnotations;

namespace BharatMedicsV2.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string AdminName { get; set; }


        [Required(ErrorMessage = "Please enter your Phone No.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 8 characters.")]
        public string Password { get; set; }

        public int Role { get; set; }
    }
}
