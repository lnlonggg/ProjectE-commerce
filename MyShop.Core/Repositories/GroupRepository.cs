using Microsoft.EntityFrameworkCore;
using MyShop.Core.Data;
using MyShop.Core.Entities;
using MyShop.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Group?> GetByIdAsync(int id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task AddAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public void Update(Group group)
        {
            _context.Groups.Update(group);
        }

        public void Delete(Group group)
        {
            _context.Groups.Remove(group);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}