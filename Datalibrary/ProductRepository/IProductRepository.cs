
using ModelLibrary;

namespace Datalibrary.ProductRepository
{
    public interface IProductRepository
    {
        Task<Product?> GetProduct(long code);
        Task<IEnumerable<Product>> GetProducts();
        Task InsertProducts(List<Product> products);
    }
}