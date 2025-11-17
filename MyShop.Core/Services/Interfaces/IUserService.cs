using MyShop.Core.DTOs;
using MyShop.Core.Entities;
using System;
using System.Threading.Tasks;

namespace MyShop.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<PagedResult<UserDto>> GetUsersAsync(int page, int pageSize, string? search);
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto> CreateUserAsync(UserCreateDto userDto);
        Task<bool> UpdateUserAsync(Guid id, UserUpdateDto userDto);
        Task<bool> DeleteUserAsync(Guid id);
    }
}