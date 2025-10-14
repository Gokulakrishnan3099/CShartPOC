using Merchant_Service.Model;

namespace Merchant_Service.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        IEnumerable<Product> GetByCategory(string category);
        IEnumerable<Product> Search(string? term); // <-- Add this method to match usage in controller
    }
}
