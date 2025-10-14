using System.ComponentModel.DataAnnotations;

namespace Merchant_Service.Model
{
    public class PaymentRequest
    {
        [Required] 
        public string CardNumber { get; set; }
        [Required]
        public string CVV { get; set; }
        [Required]
        public string ExpiryDate { get; set; } // MM/YY or MM/YYYY
        public string CardScheme { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }


    public class PaymentResponse
    {
        public string OrderId { get; set; }
        public string PaymentStatus { get; set; }
        public string Description { get; set; }
        public InvoiceDto Invoice { get; set; }
    }


    public class InvoiceDto
    {
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime IssuedAt { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
    }


    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string MaskedCard { get; set; }
        public string CardScheme { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int OrderIdRef { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime IssuedAt { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
    }
}
public enum PaymentStatus
{
    Pending,
    Approved,
    Declined
}