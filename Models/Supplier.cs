using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BharatMedicsV2.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "Please Enter Email ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please fill the Quantity of Drugs")]
        public int DrugQuantity { get; set; }

        public int DrugId { get; set; }

        [ForeignKey("DrugId")]
        public Drug Drugs {get;set;}

    }
}
