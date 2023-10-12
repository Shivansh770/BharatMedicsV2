using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BharatMedicsV2.Models
{
    public class Drug
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DrugId { get; set; }

        [Required(ErrorMessage = "Please enter the drug name")]
        public string DrugName { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage ="Please enter the price")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Please enter the expiry date")]
        public DateTime ExpiryDate { get; set; }
    }
}
