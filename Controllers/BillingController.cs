using Merchant_Service.Data;
using Merchant_Service.Model;
using Merchant_Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Merchant_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BillingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateBilling")]
        public async Task<IActionResult> CreateBilling([FromBody] BillingInfo billing)
        {
            billing.CreatedAt = DateTime.UtcNow;
            _context.BillingInfo.Add(billing);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBillingById), new { id = billing.Id }, billing);
        }


        [HttpGet("GetBillInfo")]
        public async Task<ActionResult<IEnumerable<BillingInfo>>> GetAll()
        {
            return await _context.BillingInfo.OrderByDescending(b => b.CreatedAt).ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillingById(int id)
        {
            var billing = await _context.BillingInfo.FindAsync(id);
            if (billing == null)
                return NotFound();
            return Ok(billing);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBilling(int id, [FromBody] BillingInfo updated)
        {
            var existing = await _context.BillingInfo.FindAsync(id);
            if (existing == null) return NotFound();


            existing.Name = updated.Name;
            existing.Address = updated.Address;
            existing.City = updated.City;
            existing.PostalCode = updated.PostalCode;
            existing.Country = updated.Country;
            existing.Email = updated.Email;
            existing.Phone = updated.Phone;


            await _context.SaveChangesAsync();
            return Ok(existing);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBilling(int id)
        {
            var billing = await _context.BillingInfo.FindAsync(id);
            if (billing == null) return NotFound();


            _context.BillingInfo.Remove(billing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
