namespace MarketplacePetProj.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HashPasword { get; set; }
        public List<Product> CreatedProducts { get; set; }
        public Order Order { get; set; }
    }
}
