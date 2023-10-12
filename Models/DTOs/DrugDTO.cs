using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BharatMedicsV2.Models.DTOs
{
    public class DrugDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public string Description { get; set; }

        public float Price {  get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
