using GenericFilter.DTOs.Inputs;
using GenericFilter.Models;

namespace GenericFilter.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(QueryParameters filter);
    }
}
