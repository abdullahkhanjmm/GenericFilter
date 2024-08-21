﻿using Microsoft.EntityFrameworkCore;

namespace GenericFilter
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(QueryParameters filter)
        {
            var query = _context.Products.AsQueryable();

            // Apply filtering, sorting, and pagination
            query = query.ApplyQueryParameters(filter);

            return query.ToList();
        }

    }
}
