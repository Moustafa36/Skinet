using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class PorductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public PorductRepository( StoreContext contex)
        {
            _context = contex;
            
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
            .Include(p => p.ProductBrand)
            .Include(P=>P.ProductType)
            .FirstOrDefaultAsync(P=>P.Id==id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
            .Include(p => p.ProductBrand)
            .Include(P=>P.ProductType)
            .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
           return await _context.ProductTypes.ToListAsync();
        }
    }
}