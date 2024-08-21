using Microsoft.EntityFrameworkCore;

namespace GenericFilter
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(PaginationFilter filter)
        {
            var query = _context.Products.AsQueryable();

            // Apply filtering, sorting, and pagination
            query = query.Process(filter);

            return query.ToList();
        }

    }
}
