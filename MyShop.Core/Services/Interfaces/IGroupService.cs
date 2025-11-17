using MyShop.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetAllGroupsAsync();
        Task<GroupDto?> GetGroupByIdAsync(int id);
        Task<GroupDto> CreateGroupAsync(GroupCreateDto groupDto);
        Task<bool> UpdateGroupAsync(int id, GroupCreateDto groupDto);
        Task<bool> DeleteGroupAsync(int id);
    }
}