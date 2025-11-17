using Microsoft.EntityFrameworkCore;
using MyShop.Core.Data;
using MyShop.Core.Entities;
using MyShop.Core.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public IQueryable<Product> GetAllQueryable()
        {
            return _context.Products.AsQueryable();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}