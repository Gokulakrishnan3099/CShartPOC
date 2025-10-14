using Merchant_Service.Model;

namespace Merchant_Service.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;


        public ProductRepository()
        {
            _products = new List<Product>
                {
                new Product { Id = 1, Name = "Health / Medical Insurance", Category = "Health", Description = "Comprehensive inpatient & outpatient coverage" },
                new Product { Id = 2, Name = "Dental Insurance", Category = "Dental", Description = "Preventive and restorative dental plans" },
                new Product { Id = 3, Name = "Vision / Eye Care", Category = "Vision", Description = "Routine eye exams, lenses and frames" },
                new Product { Id = 4, Name = "Pharmacy / Prescription Drug Plans", Category = "Pharmacy", Description = "Prescription drug coverage and formularies" },
                new Product { Id = 5, Name = "Supplemental Health Insurance", Category = "Supplemental", Description = "Accident, Critical Illness and Hospital Indemnity" },
                new Product { Id = 6, Name = "Medicare Advantage (Part C)", Category = "Medicare", Description = "Medicare Advantage plans (HMO/PPO)" },
                new Product { Id = 7, Name = "Medicare Supplement (Medigap)", Category = "Medicare", Description = "Medigap plans to cover original Medicare gaps" },
                new Product { Id = 8, Name = "Medicare Part D", Category = "Medicare", Description = "Standalone prescription drug plans for Medicare beneficiaries" },
                new Product { Id = 9, Name = "International Health / Global Medical Plans", Category = "International", Description = "Travel and expatriate medical coverage" },
                new Product { Id = 10, Name = "Savings & Spending Accounts (HRA / HSA / FSA)", Category = "Accounts", Description = "Tax-advantaged accounts and administration" },
                new Product { Id = 11, Name = "Behavioral Health & Wellness", Category = "Behavioral", Description = "Mental health, EAP and wellness services" }
                };
        }


        public IEnumerable<Product> GetAll() => _products;


        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);


        public IEnumerable<Product> GetByCategory(string category) =>
        _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));


        public IEnumerable<Product> Search(string? term)
        {
            if (string.IsNullOrWhiteSpace(term)) return _products;
            term = term.Trim();
            return _products.Where(p => p.Name.Contains(term, StringComparison.OrdinalIgnoreCase)
            || p.Description.Contains(term, StringComparison.OrdinalIgnoreCase)
            || p.Category.Contains(term, StringComparison.OrdinalIgnoreCase));
        }
    }
}
