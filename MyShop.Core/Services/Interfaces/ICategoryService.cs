using MyShop.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryDto);
        Task<bool> UpdateCategoryAsync(int id, CategoryCreateDto categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}