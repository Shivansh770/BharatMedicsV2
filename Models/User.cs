using System.ComponentModel.DataAnnotations;

namespace BharatMedicsV2.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage ="Please Provide User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Please enter your email address")]
        public string Email { get; set; }

        public string Role {  get; set; }

        
        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 8 characters.")]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
