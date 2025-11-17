using MyShop.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group?> GetByIdAsync(int id);
        Task<IEnumerable<Group>> GetAllAsync();
        Task AddAsync(Group group);
        void Update(Group group);
        void Delete(Group group);
        Task<bool> SaveChangesAsync();
    }
}