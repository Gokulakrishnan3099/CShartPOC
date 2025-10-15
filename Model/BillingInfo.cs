using System.ComponentModel.DataAnnotations;

namespace Merchant_Service.Model
{
    public class BillingInfo
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;


        [MaxLength(500)]
        public string? Address { get; set; }


        [MaxLength(100)]
        public string? City { get; set; }


        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }


        [EmailAddress]
        public string? Email { get; set; }


        [Phone]
        public string? Phone { get; set; }


        public DateTime? CreatedAt { get; set; }
    }
}
