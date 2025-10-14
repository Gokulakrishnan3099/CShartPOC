using Merchant_Service.Data;
using Merchant_Service.Helper;
using Merchant_Service.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Merchant_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _db;


        public PaymentController(AppDbContext db)
        {
            _db = db;
        }


        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (string.IsNullOrWhiteSpace(req.CardNumber) || string.IsNullOrWhiteSpace(req.CVV) || req.Amount <= 0)
                return BadRequest(new { error = "missing or invalid fields" });


            bool shouldApprove = PaymentHelper.SimpleApprovalLogic(req);


            var order = new Order
            {
                OrderId = Guid.NewGuid().ToString(),
                CardScheme = req.CardScheme,
                MaskedCard = PaymentHelper.MaskCard(req.CardNumber),
                Amount = req.Amount,
                Currency = req.Currency ?? "USD",
                PaymentStatus = shouldApprove ? PaymentStatus.Approved : PaymentStatus.Declined,
                Description = shouldApprove ? "Payment successful" : "Payment declined by gateway",
                CreatedAt = DateTime.UtcNow
            };


            var invoice = new Invoice
            {
                InvoiceNumber = PaymentHelper.GenerateInvoiceNumber(),
                Order = order,
                BillingName = req.BillingName,
                BillingAddress = req.BillingAddress,
                Amount = req.Amount,
                Currency = order.Currency,
                IssuedAt = DateTime.UtcNow
            };


            _db.Orders.Add(order);
            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();


            var response = new PaymentResponse
            {
                OrderId = order.OrderId,
                PaymentStatus = order.PaymentStatus.ToString(),
                Description = order.Description,
                Invoice = new InvoiceDto
                {
                    InvoiceNumber = invoice.InvoiceNumber,
                    Amount = invoice.Amount,
                    Currency = invoice.Currency,
                    IssuedAt = invoice.IssuedAt,
                    BillingName = invoice.BillingName,
                    BillingAddress = invoice.BillingAddress
                }
            };


            return Ok(response);
        }
    }

}

