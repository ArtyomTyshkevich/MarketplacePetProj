namespace MarketplacePetProj.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
