using System.ComponentModel.DataAnnotations;

namespace BharatMedicsV2.Models.DTOs
{
    public class SupplierDTO
    {
        public int SupplierId { get; set; }

  
        public string SupplierName { get; set; }

  
        public string Email { get; set; }


        public int DrugQuantity { get; set; }

        public int DrugId { get; set; }
    }
}
