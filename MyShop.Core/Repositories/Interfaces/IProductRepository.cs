using MyShop.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Core.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        IQueryable<Product> GetAllQueryable();
        Task AddAsync(Product product);
        void Update(Product product);
        void Delete(Product product);
        Task<bool> SaveChangesAsync();
    }
}