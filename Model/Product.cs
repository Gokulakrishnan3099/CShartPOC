namespace Merchant_Service.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // e.g., Health, Dental, Vision, Medicare, ...
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
