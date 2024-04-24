using System.ComponentModel.DataAnnotations;

namespace MarketplacePetProj.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public DateTime CreatedDate { get; set; }
        public Order()
        { 
            Products=new List<Product>();
        }
    }
}
