using MyShop.Core.DTOs;
using MyShop.Core.Entities;
using MyShop.Core.Repositories.Interfaces;
using MyShop.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<GroupDto>> GetAllGroupsAsync()
        {
            var groups = await _groupRepository.GetAllAsync();
            return groups.Select(g => new GroupDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                CategoryId = g.CategoryId
            });
        }

        public async Task<GroupDto?> GetGroupByIdAsync(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            if (group == null) return null;

            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                CategoryId = group.CategoryId
            };
        }

        public async Task<GroupDto> CreateGroupAsync(GroupCreateDto groupDto)
        {
            var group = new Group
            {
                Name = groupDto.Name,
                Description = groupDto.Description,
                CategoryId = groupDto.CategoryId
            };

            await _groupRepository.AddAsync(group);
            await _groupRepository.SaveChangesAsync();

            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                CategoryId = group.CategoryId
            };
        }

        public async Task<bool> UpdateGroupAsync(int id, GroupCreateDto groupDto)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            if (group == null) return false;

            group.Name = groupDto.Name;
            group.Description = groupDto.Description;
            group.CategoryId = groupDto.CategoryId;

            _groupRepository.Update(group);
            return await _groupRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteGroupAsync(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            if (group == null) return false;

            _groupRepository.Delete(group);
            return await _groupRepository.SaveChangesAsync();
        }
    }
}