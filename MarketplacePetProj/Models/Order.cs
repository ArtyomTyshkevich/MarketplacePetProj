using MarketplacePetProj.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketplacePetProj.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public DateTime CreatedDate { get; set; }
        public OrderStatus orderStatus { get; set; } = Enums.OrderStatus.basket;
        public string CLientId { get; set; }

    }
}
