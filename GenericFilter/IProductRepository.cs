namespace GenericFilter
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(PaginationFilter filter);
    }
}
