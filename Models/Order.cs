using System.ComponentModel.DataAnnotations;

namespace BharatMedicsV2.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfOrder { get; set; } = DateTime.Now;

        public int QuantityBooked { get; set; }

        public int TotalAmount { get; set; }

        public string Payment_Status { get; set; }

        public bool IsVerified { get; set; }

        public List<Cart> Carts { get; set; }


    }
}
