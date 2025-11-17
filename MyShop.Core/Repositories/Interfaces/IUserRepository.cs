using MyShop.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username);
        IQueryable<User> GetAllQueryable();
        Task AddAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task<bool> SaveChangesAsync();
    }
}