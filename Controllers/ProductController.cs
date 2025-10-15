using Merchant_Service.Model;
using Merchant_Service.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Merchant_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;


        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }


        // GET api/products
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll([FromQuery] string? q)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return Ok(_repo.Search(q));


            return Ok(_repo.GetAll());
        }


        // GET api/products/{id}
        [Authorize]
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _repo.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }


        // GET api/products/category/{category}
        [Authorize]
        [HttpGet("category/{category}")]
        public ActionResult<IEnumerable<Product>> GetByCategory(string category)
        {
            var items = _repo.GetByCategory(category);
            return Ok(items);
        }


        // GET api/products/healthcare-medicaresummary (example of friendly route)
        [HttpGet("summary/medicare")]
        public ActionResult<IEnumerable<Product>> GetMedicareSummary()
        {
            var items = _repo.GetByCategory("Medicare");
            return Ok(items);
        }
    }
}
