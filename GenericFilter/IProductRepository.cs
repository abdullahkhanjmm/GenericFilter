namespace GenericFilter
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(QueryParameters filter);
    }
}
